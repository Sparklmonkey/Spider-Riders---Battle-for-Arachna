using UnityEngine;

public class AnimateClimbDownRightInteractable : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        var tile = GetComponent<OverlayTile>();
        MapManager.Instance.MovePlayerToTile(new Vector2Int(tile.tileLocation.x - 4, tile.tileLocation.y - 6));
    }

    public void OnUseItem(string item)
    {
        return;
    }

    public void Setup()
    {
        return;
    }
}
