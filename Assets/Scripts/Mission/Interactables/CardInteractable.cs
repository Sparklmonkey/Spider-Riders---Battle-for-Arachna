using UnityEngine;

public class CardInteractable : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        TestPlayer<PlayerData>.AddItemToInventory(gameObject.name.Replace("Item_", ""));
        Destroy(gameObject);
    }
}