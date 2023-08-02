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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        _originalParent = transform.parent;
        _initialPosition = _originalParent.transform.position;
    }

    public void StartDrag()
    {
        IsClicked = true;
        transform.parent = MapSceneManager.Instance.transform;
        MissionInventoryManager.Instance.CloseInventory(false);
    }

    public void SetupInventoryItem(string itemName)
    {
        name = itemName;
        spriteRenderer.sprite = Resources.Load<Sprite>($"Sprites/Items/{itemName}");
    }

    public void EndDrag()
    {
        IsClicked = false;
        MouseController.UseItemFromInventory(name);
        MissionInventoryManager.Instance.IsInventoryOpen = false;
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
