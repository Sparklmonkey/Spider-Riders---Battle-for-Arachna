using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour
{
    public bool IsClicked;
    private Vector3 _initialPosition;
    private Transform _originalParent;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public event Action<string> OnUseItem;
    public event Action<bool> OnDragStart;

    private void Awake()
    {
        _originalParent = transform.parent;
        _initialPosition = _originalParent.transform.position;
    }

    private void OnMouseDown()
    {
        IsClicked = true;
        transform.parent = MapSceneManager.Instance.transform;
        OnDragStart?.Invoke(false);
    }

    private void OnMouseDrag()
    {
    }

    private void OnMouseUp()
    {
        EndDrag();
    }

    public void SetupInventoryItem(string itemName)
    {
        name = itemName;
        spriteRenderer.sprite = Resources.Load<Sprite>($"Sprites/Items/{itemName}");
    }

    public void EndDrag()
    {
        IsClicked = false;
        OnUseItem?.Invoke(name);
        OverworldUIManager.Instance.IsInventoryOpen = false;
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (IsClicked)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        }
    }
}
