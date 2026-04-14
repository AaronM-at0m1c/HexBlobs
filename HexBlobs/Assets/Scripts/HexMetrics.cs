using UnityEngine;

// Thank you to Soul's Game Dev Journey on youtube for their Hex Tile how-to videos

public static class HexMetrics
{
    public static float OuterRadius(float hexSize)
    {
        return hexSize;
    }

    public static float InnerRadius(float hexSize)
    {
        return hexSize * 0.866025404f;
    }

    public static Vector3[] Corners(float hexSize, HexOrientation orientation)
    {
        Vector3[] corners = new Vector3[6];
        for (int i = 0; i < 6; i++)
        {
            corners[i] = Corner(hexSize, orientation, i);
        }
        return corners;
    }

    public static Vector3 Corner(float hexSize, HexOrientation orientation, int index)
    {
        float angle = 60f * index;
        if (orientation == HexOrientation.PointyTop)
        {
            angle += 30f;
        }
        Vector3 corner = new Vector3(
            hexSize * Mathf.Cos(angle * Mathf.Deg2Rad),
            hexSize * Mathf.Sin(angle * Mathf.Deg2Rad),
            0f
        );
        return corner;
    }

    public static Vector3 Center(float hexSize, int x, int z, HexOrientation orientation)
    {
        Vector3 centerPosition;
        if (orientation == HexOrientation.PointyTop)
        {
            centerPosition.x = (x + z * 0.5f - z / 2) * (InnerRadius(hexSize) * 2f);
            centerPosition.y = z * (OuterRadius(hexSize) * 1.5f);
            centerPosition.z = 0f;
        }
        else
        {
            centerPosition.x = x * (OuterRadius(hexSize) * 1.5f);
            centerPosition.y = (z + x * 0.5f - x / 2) * (InnerRadius(hexSize) * 2f);
            centerPosition.z = 0f;
        }
        return centerPosition;
    }
}