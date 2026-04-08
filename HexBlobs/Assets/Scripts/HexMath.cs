using System.Collections.Generic;
using UnityEngine;

public static class HexMath
{
    public static readonly Vector2Int[] Directions =
    {
        new Vector2Int(1, 0),
        new Vector2Int(1, -1),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),
        new Vector2Int(0, 1)
    };

    public static int Distance(Vector2Int a, Vector2Int b)
    {
        int dx = a.x - b.x;
        int dz = a.y - b.y;
        int dy = -dx - dz;

        return (Mathf.Abs(dx) + Mathf.Abs(dy) + Mathf.Abs(dz)) / 2;
    }

    public static List<Vector2Int> GetNeighbors(Vector2Int coord)
    {
        List<Vector2Int> neighbors = new();

        foreach (var dir in Directions)
        {
            neighbors.Add(coord + dir);
        }

        return neighbors;
    }
}