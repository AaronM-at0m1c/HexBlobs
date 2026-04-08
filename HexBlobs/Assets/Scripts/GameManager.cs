using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerId CurrentPlayer { get; private set; } = PlayerId.Player1;
    public BoardState Board { get; private set; }

    private HexGrid grid;
    private HexTile currentHoverTile;


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

        Board.GetTile(6, 4).Owner = PlayerId.Player2;
        Board.GetTile(7, 4).Owner = PlayerId.Player2;
    }

    public void OnTileClicked(HexTile clickedTile)
    {
        Vector2Int target = new(clickedTile.X, clickedTile.Z);
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