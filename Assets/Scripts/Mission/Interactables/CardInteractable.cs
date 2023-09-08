using UnityEngine;

public class CardInteractable : MonoBehaviour, IInteractable
{
    private GameObject _childObject;
    public void OnInteract()
    {
        TestPlayer<PlayerData>.AddItemToInventory(gameObject.name.Replace("Item_", ""));
        Destroy(_childObject);
        Destroy(this);
    }

    public void OnUseItem(string item)
    {
        return;
    }

    public void Setup()
    {
        _childObject = Instantiate(Resources.Load<GameObject>($"Prefabs/Items/Card"), transform);
        _childObject.transform.position = transform.position;
    }
}