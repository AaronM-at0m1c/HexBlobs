using UnityEngine;
using System.Collections.Generic;

// Thank you to Soul's Game Dev Journey on youtube for their Hex Tile how-to videos

public class HexGrid : MonoBehaviour
{
    [field: SerializeField] public HexOrientation Orientation { get; private set; }
    [field: SerializeField] public int Width { get; private set; }
    [field: SerializeField] public int Height { get; private set; }
    [field: SerializeField] public float HexSize { get; private set; }
    [field: SerializeField] public GameObject HexPrefab { get; private set; }

    private Dictionary<Vector2Int, HexTile> tileMap = new();
    public IReadOnlyDictionary<Vector2Int, HexTile> TileMap => tileMap;

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (int z = 0; z < Height; z++)
        {
            for (int x = 0; x < Width; x++)
            {
                CreateHex(x, z);
            }
        }

        GameManager.Instance.InitializeBoardFromGrid(this);
    }

    private void CreateHex(int x, int z)
    {
        Vector3 centerPosition = HexMetrics.Center(HexSize, x, z, Orientation) + transform.position;
        GameObject hex = Instantiate(HexPrefab, centerPosition, Quaternion.identity, transform);

        HexTile tile = hex.GetComponent<HexTile>();
        tile.Init(x, z, HexSize, Orientation);

        tileMap[new Vector2Int(x, z)] = tile;
    }

    public HexTile GetHexTile(int x, int z)
    {
        tileMap.TryGetValue(new Vector2Int(x, z), out HexTile tile);
        return tile;
    }

    private void OnDrawGizmos()
    {
        for (int z = 0; z < Height; z++)
        {
            for (int x = 0; x < Width; x++)
            {
                Vector3 centerPosition = HexMetrics.Center(HexSize, x, z, Orientation) + transform.position;
                for (int s = 0; s < HexMetrics.Corners(HexSize, Orientation).Length; s++)
                {
                    Gizmos.DrawLine(
                        centerPosition + HexMetrics.Corners(HexSize, Orientation)[s % 6],
                        centerPosition + HexMetrics.Corners(HexSize, Orientation)[(s + 1) % 6]
                    );
                }
            }
        }
    }
}
