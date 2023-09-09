using UnityEngine;

public class AnimateClimbUpLeftInteractable : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        var tile = GetComponent<OverlayTile>();
        MapManager.Instance.MovePlayerToTile(new Vector2Int(tile.tileLocation.x - 4, tile.tileLocation.y - 4));
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
