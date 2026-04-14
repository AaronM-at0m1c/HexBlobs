using System;
using UnityEngine;
using Unity.Netcode;
using UnityEditor.PackageManager;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerId CurrentPlayer { get; private set; } = PlayerId.Player1;
    public BoardState Board { get; private set; }

    private HexGrid grid;
    private HexTile currentHoverTile;

    public event Action<int> OnBoardChanged;
    private int score = 0;
    private NetworkVariable<int> lastMoveX = new NetworkVariable<int>(-1);
    private NetworkVariable<int> lastMoveZ = new NetworkVariable<int>(-1);


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Board = new BoardState();
        DontDestroyOnLoad(gameObject);
    }

    public void InitializeBoardFromGrid(HexGrid hexGrid)
{
    grid = hexGrid;

    foreach (var pair in grid.TileMap)
    {
        Vector2Int coord = pair.Key;
        Board.AddTile(coord.x, coord.y);
    }

    InitializeStartingPositions();
    RefreshVisuals();
}

    private void InitializeStartingPositions()
    {
        Board.GetTile(0, 0).Owner = PlayerId.Player1;
        Board.GetTile(1, 0).Owner = PlayerId.Player1;

        Board.GetTile(9, 4).Owner = PlayerId.Player2;
        Board.GetTile(8, 4).Owner = PlayerId.Player2;
    }

    public void OnTileClicked(HexTile clickedTile)
    {
        if (!IsServer) return;

        lastMoveX.Value = clickedTile.X;
        lastMoveZ.Value = clickedTile.Z;

    }

    private void ExecuteMove(int x, int z)
{
    HexTile tile = grid.GetHexTile(x, z);
    if (tile == null) return;

    Vector2Int target = new(x, z);
    MoveDecision decision = BlobRules.ClassifyMove(Board, CurrentPlayer, target);

    switch (decision.MoveType)
    {
        case MoveType.Flip:
            new FlipCommand(CurrentPlayer, target).Execute(Board);
            EndTurn();
            RefreshVisuals();
            break;

        case MoveType.Jump:
            new JumpCommand(CurrentPlayer, decision.JumpOffset).Execute(Board);
            EndTurn();
            RefreshVisuals();
            break;

        default:
            Debug.Log("Invalid move.");
            break;
    }
}

     public override void OnNetworkSpawn()
    {
        lastMoveX.OnValueChanged += (oldVal, newVal) => ExecuteMove(lastMoveX.Value, lastMoveZ.Value);
        lastMoveZ.OnValueChanged += (oldVal, newVal) => ExecuteMove(lastMoveX.Value, lastMoveZ.Value);
    }

    public void RefreshVisuals()
    {
        foreach (var tileState in Board.AllTiles())
        {
            HexTile viewTile = grid.GetHexTile(tileState.X, tileState.Z);
            if (viewTile != null)
            {
                viewTile.SetOwner(tileState.Owner);
            }
        }
        OnBoardChanged?.Invoke(score);
    }

    private void EndTurn()
    {
        CurrentPlayer = CurrentPlayer == PlayerId.Player1
            ? PlayerId.Player2
            : PlayerId.Player1;

        Debug.Log("Current player: " + CurrentPlayer);
    }

    public void OnTileSelected(HexTile tile)
    {
        Debug.Log($"Tile clicked at ({tile.X}, {tile.Z})");
    }
}