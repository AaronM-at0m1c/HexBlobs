using UnityEngine;
using UnityEngine.InputSystem;

public class TileClickTester : MonoBehaviour
{
    private HexTile currentlyHoveredTile;
    private HexTile currentlySelectedTile;

    void Update()
    {
        if (Mouse.current == null || Camera.main == null)
            return;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0f)
        );

        Vector2 point = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
        Collider2D hit = Physics2D.OverlapPoint(point);

        HexTile hoveredTile = null;
        if (hit != null)
        {
            hoveredTile = hit.GetComponent<HexTile>();
        }

        if (currentlyHoveredTile != hoveredTile)
        {
            if (currentlyHoveredTile != null)
                currentlyHoveredTile.SetHover(false);

            currentlyHoveredTile = hoveredTile;

            if (currentlyHoveredTile != null)
                currentlyHoveredTile.SetHover(true);
        }

        if (Mouse.current.leftButton.wasPressedThisFrame && hoveredTile != null)
        {
            if (currentlySelectedTile != null && currentlySelectedTile != hoveredTile)
            {
                currentlySelectedTile.SetSelected(false);
            }

            currentlySelectedTile = hoveredTile;
            currentlySelectedTile.SetSelected(true);

            Debug.Log($"Selected tile at X={hoveredTile.X}, Z={hoveredTile.Z}");

            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnTileClicked(hoveredTile);
            }
            else
            {
                Debug.LogWarning("GameManager.Instance is null.");
            }
        }
    }
}