using UnityEngine;

public class ActionRopeRightInteractable : MonoBehaviour, IInteractable
{
    private GameObject _childObject;

    public void OnInteract()
    {
        return;
    }

    public void OnUseItem(string item)
    {
        if (item != "Rope") { return; }

        TestPlayer<PlayerData>.RemoveItemFromInventory("Rope");

        MapSceneManager.Instance.AddRopeUsedImage();

        var tileScript = GetComponent<OverlayTile>();

        MapManager.Instance.ChangeTileRopeUsed(new Vector2Int(tileScript.tileLocation.x, tileScript.tileLocation.y),
                        new Vector2Int(tileScript.tileLocation.x + 4, tileScript.tileLocation.y + 3), true);

        Destroy(_childObject);
        Destroy(this);
    }

    public void Setup()
    {
        _childObject = Instantiate(Resources.Load<GameObject>("Prefabs/Items/Action_Anim"), transform);
        _childObject.transform.position = transform.position;
    }
}
