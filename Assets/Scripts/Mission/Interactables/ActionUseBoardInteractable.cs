using UnityEngine;

public class ActionUseBoardInteractable : MonoBehaviour, IInteractable
{
    private GameObject _childObject;
    public void OnInteract()
    {
        return;
    }

    public void OnUseItem(string item)
    {
        if (item != "Board") { return; }

        var tileScript = GetComponent<OverlayTile>();

        TestPlayer<PlayerData>.RemoveItemFromInventory("Board");
        MapSceneManager.Instance.AddBoardUsedImage();

        MapManager.Instance.ChangeTileToWalkable(new Vector2Int(tileScript.tileLocation.x - 1, tileScript.tileLocation.y));
        MapManager.Instance.ChangeTileToWalkable(new Vector2Int(tileScript.tileLocation.x - 2, tileScript.tileLocation.y));
        MapManager.Instance.ChangeTileToWalkable(new Vector2Int(tileScript.tileLocation.x - 3, tileScript.tileLocation.y));
        MapManager.Instance.ChangeTileToBoardUsed(new Vector2Int(tileScript.tileLocation.x, tileScript.tileLocation.y));

        Destroy(_childObject);
        Destroy(this);
    }

    public void Setup()
    {
        _childObject = Instantiate(Resources.Load<GameObject>("Prefabs/Items/Action_Anim"), transform);
        _childObject.transform.position = transform.position;
    }
}
