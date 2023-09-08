using UnityEngine;

public class ActionDoorInteractable : MonoBehaviour, IInteractable
{
    private GameObject _childObject;
    public void OnInteract()
    {
        return;
    }

    public void OnUseItem(string item)
    {
        if (item != "Key") { return; }

        var tileScript = GetComponent<OverlayTile>();
        MapManager.Instance.ChangeTileToOpenDoor(new Vector2Int(tileScript.tileLocation.x, tileScript.tileLocation.y));
        MapManager.Instance.ChangeTileToWalkable(new Vector2Int(tileScript.tileLocation.x, tileScript.tileLocation.y + 1));
        MapSceneManager.Instance.ChangeDoorToOpen();

        Destroy(_childObject);
        Destroy(this);
    }

    public void Setup()
    {
        _childObject = Instantiate(Resources.Load<GameObject>("Prefabs/Items/Action_Anim"), transform);
        _childObject.transform.position = transform.position;
    }
}
