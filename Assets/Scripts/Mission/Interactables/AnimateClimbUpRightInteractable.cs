using UnityEngine;

public class AnimateClimbUpRightInteractable : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        var tile = GetComponent<OverlayTile>();
        MapManager.Instance.MovePlayerToTile(new Vector2Int(tile.tileLocation.x + 3, tile.tileLocation.y + 5));
    }

    public void OnUseItem(string item)
    {
        return;
    }

    public void Setup()
    {
        MapSceneManager.Instance.AddRopeUsedImage();
    }
}
