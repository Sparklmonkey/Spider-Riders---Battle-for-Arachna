using UnityEngine;
using UnityEngine.Events;

public class SimpleSpriteButton : MonoBehaviour
{
    [SerializeField] private UnityEvent OnSpriteClick;

    private void OnMouseDown()
    {
        OnSpriteClick?.Invoke();
    }
}
