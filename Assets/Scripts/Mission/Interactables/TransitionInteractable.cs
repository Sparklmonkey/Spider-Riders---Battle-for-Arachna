using UnityEngine;

public class TransitionInteractable : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        int nextScene = int.Parse(gameObject.name.Replace("Transition_", ""));
        MapSceneManager.Instance.SetupMapScene(nextScene);
        MapManager.Instance.SetupMap(nextScene - 1);
    }
}