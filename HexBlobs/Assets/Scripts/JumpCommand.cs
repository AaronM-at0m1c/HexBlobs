using System.Collections.Generic;
using UnityEngine;

// Thank you to GenAI for helping me figure out how to use command functionality

public class JumpCommand
{
    public PlayerId Player;
    public Vector2Int Offset;

    public JumpCommand(PlayerId player, Vector2Int offset)
    {
        Player = player;
        Offset = offset;
    }

    public void Execute(BoardState board)
    {
        List<TileState> blob = board.GetPlayerTiles(Player);
        List<Vector2Int> destinations = new();

        foreach (var tile in blob)
        {
            destinations.Add(new Vector2Int(tile.X + Offset.x, tile.Z + Offset.y));
        }

        foreach (var tile in blob)
        {
            tile.Owner = PlayerId.None;
        }

        List<TileState> newBlobTiles = new();

        foreach (var destination in destinations)
        {
            TileState destTile = board.GetTile(destination.x, destination.y);
            destTile.Owner = Player;
            newBlobTiles.Add(destTile);
        }

        FlipAdjacentEnemies(board, newBlobTiles);
    }

    private void FlipAdjacentEnemies(BoardState board, List<TileState> newBlobTiles)
    {
        PlayerId enemy = Player == PlayerId.Player1 ? PlayerId.Player2 : PlayerId.Player1;

        foreach (var blobTile in newBlobTiles)
        {
            Vector2Int coord = new(blobTile.X, blobTile.Z);

            foreach (var neighbor in HexMath.GetNeighbors(coord))
            {
                TileState tile = board.GetTile(neighbor.x, neighbor.y);

                if (tile != null && tile.Owner == enemy)
                {
                    tile.Owner = Player;
                }
            }
        }
    }
}