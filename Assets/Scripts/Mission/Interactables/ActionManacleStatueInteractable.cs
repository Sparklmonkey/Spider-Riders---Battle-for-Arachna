using UnityEngine;

public class ActionManacleStatueInteractable : MonoBehaviour, IInteractable
{
    private GameObject _childObject;

    public void OnInteract()
    {
        return;
    }

    public void OnUseItem(string item)
    {
        if (item != "Manacle") { return; }

        TestPlayer<PlayerData>.RemoveItemFromInventory("Manacle");
        MapSceneManager.Instance.OpenManaclePortal();

        var tileScript = GetComponent<OverlayTile>();
        MapManager.Instance.OpenPortal();
        MapManager.Instance.ChangeTileToWalkable(new Vector2Int(tileScript.tileLocation.x, tileScript.tileLocation.y + 1));
        Destroy(_childObject);
        Destroy(this);
    }

    public void Setup()
    {
        _childObject = Instantiate(Resources.Load<GameObject>("Prefabs/Items/Statue"), transform);
        _childObject.transform.position = transform.position;
        var temp = Instantiate(Resources.Load<GameObject>("Prefabs/Items/Action_Anim"), _childObject.transform);
        temp.transform.position = transform.position;
    }
}