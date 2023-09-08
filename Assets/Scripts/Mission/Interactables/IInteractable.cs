using UnityEngine;

public interface IInteractable
{
    public void Setup();
    public void OnInteract();
    public void OnUseItem(string item);
}
