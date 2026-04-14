using System.Collections.Generic;
using UnityEngine;

// Thank you to Soul's Game Dev Journey on youtube for their Hex Tile how-to videos

public class BoardState
{
    private Dictionary<Vector2Int, TileState> tiles = new();

    public void AddTile(int x, int z)
    {
        tiles[new Vector2Int(x, z)] = new TileState
        {
            X = x,
            Z = z,
            Owner = PlayerId.None
        };
    }

    public TileState GetTile(int x, int z)
    {
        tiles.TryGetValue(new Vector2Int(x, z), out TileState tile);
        return tile;
    }

    public bool HasTile(int x, int z)
    {
        return tiles.ContainsKey(new Vector2Int(x, z));
    }

    public IEnumerable<TileState> AllTiles()
    {
        return tiles.Values;
    }

    public List<TileState> GetPlayerTiles(PlayerId player)
    {
        List<TileState> result = new();

        foreach (var tile in tiles.Values)
        {
            if (tile.Owner == player)
                result.Add(tile);
        }

        return result;
    }
}