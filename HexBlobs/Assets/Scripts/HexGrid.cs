using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [field: SerializeField] public HexOrientation Orientation { get; private set; }
    [field: SerializeField] public int Width { get; private set; }
    [field: SerializeField] public int Height { get; private set; }
    [field: SerializeField] public float HexSize { get; private set; }
    [field: SerializeField] public GameObject HexPrefab { get; private set; }

    private void Start()
    {
        for (int z = 0; z < Height; z++)
        {
            for (int x = 0; x < Width; x++)
            {
                CreateHex(x, z);
            }
        }
    }

    private void CreateHex(int x, int z)
    {
        Vector3 centerPosition = HexMetrics.Center(HexSize, x, z, Orientation) + transform.position;
        GameObject tile = Instantiate(HexPrefab, centerPosition, Quaternion.identity, transform);
        tile.GetComponent<HexTile>().Init(x, z, HexSize, Orientation);
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
