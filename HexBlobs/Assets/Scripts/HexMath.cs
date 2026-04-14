using System.Collections.Generic;
using UnityEngine;

// Thank you to Soul's Game Dev Journey on youtube for their Hex Tile how-to videos

public static class HexMath
{

    private static Vector2Int OffsetToAxial(Vector2Int offset)
    {
        int q = offset.x - (offset.y - (offset.y & 1)) / 2;
        int r = offset.y;
        return new Vector2Int(q, r);
    }

    public static int Distance(Vector2Int a, Vector2Int b)
    {
        Vector2Int ac = OffsetToAxial(a);
        Vector2Int bc = OffsetToAxial(b);

        int dx = ac.x - bc.x;
        int dz = ac.y - bc.y;
        int dy = -dx - dz;

        return (Mathf.Abs(dx) + Mathf.Abs(dy) + Mathf.Abs(dz)) / 2;
    }

    public static List<Vector2Int> GetNeighbors(Vector2Int offsetCoord)
    {
        List<Vector2Int> neighbors = new();

        // Flat-top offset neighbor offsets depend on whether the column is even or odd
        int col = offsetCoord.x;
        int row = offsetCoord.y;
        bool oddCol = (col & 1) == 1;

        Vector2Int[] dirs = oddCol
            ? new Vector2Int[]
            {
                new(1,  0), new(1,  1), new(0,  1),
                new(-1, 1), new(-1, 0), new(0, -1)
            }
            : new Vector2Int[]
            {
                new(1, -1), new(1,  0), new(0,  1),
                new(-1, 0), new(-1,-1), new(0, -1)
            };

        foreach (var dir in dirs)
            neighbors.Add(new Vector2Int(col + dir.x, row + dir.y));

        return neighbors;
    }
}