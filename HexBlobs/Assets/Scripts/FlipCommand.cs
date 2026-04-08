using UnityEngine;

// Thank you to GenAI for helping me figure out how to use command functionality

public class FlipCommand
{
    public PlayerId Player;
    public Vector2Int Target;

    public FlipCommand(PlayerId player, Vector2Int target)
    {
        Player = player;
        Target = target;
    }

    public void Execute(BoardState board)
    {
        TileState targetTile = board.GetTile(Target.x, Target.y);
        targetTile.Owner = Player;

        FlipAdjacentEnemies(board);
    }

    private void FlipAdjacentEnemies(BoardState board)
    {
        PlayerId enemy = Player == PlayerId.Player1 ? PlayerId.Player2 : PlayerId.Player1;

        foreach (var neighbor in HexMath.GetNeighbors(Target))
        {
            TileState tile = board.GetTile(neighbor.x, neighbor.y);

            if (tile != null && tile.Owner == enemy)
            {
                tile.Owner = Player;
            }
        }
    }
}
