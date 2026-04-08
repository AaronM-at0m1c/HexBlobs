using UnityEngine;

public class HexTile : MonoBehaviour
{
    public int X { get; private set; }
    public int Z { get; private set; }

    private SpriteRenderer spriteRenderer;
    private Color defaultColor = Color.white;

    [SerializeField] private Color highlightColor = Color.yellow;
    [SerializeField] private Color selectedColor = Color.green;

    private bool isSelected = false;

    public void Init(int x, int z, float hexSize, HexOrientation orientation)
    {
        X = x;
        Z = z;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            defaultColor = spriteRenderer.color;
        }

        Vector3[] corners3D = HexMetrics.Corners(hexSize, orientation);
        Vector2[] corners2D = System.Array.ConvertAll(corners3D, v => new Vector2(v.x, v.y));

        PolygonCollider2D col = gameObject.AddComponent<PolygonCollider2D>();
        col.SetPath(0, corners2D);
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;

        if (spriteRenderer == null)
            return;

        spriteRenderer.color = selected ? selectedColor : defaultColor;
    }

    public void SetHover(bool hovering)
    {
        if (spriteRenderer == null || isSelected)
            return;

        spriteRenderer.color = hovering ? highlightColor : defaultColor;
    }
}