using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OverworldUIManager : MonoBehaviour
{
    private static OverworldUIManager _instance;
    public static OverworldUIManager Instance
    {
        get { return _instance; }
    }

    public bool IsInventoryOpen;

    [SerializeField]
    private MissionInventoryManager inventoryManager;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }



    public void UseItemFromInventory(string item)
    {
        var focusedTileHit = GetFocusOnTileUnderSprite();

        if (focusedTileHit.HasValue)
        {
            var overlayTile = focusedTileHit.Value.collider.gameObject;
            var tileScript = overlayTile.GetComponent<OverlayTile>();

            var interactable = tileScript.GetComponent<IInteractable>();
            if(interactable == null) { return; }
            interactable.OnUseItem(item);
        }
    }

    public RaycastHit2D? GetFocusOnTileUnderSprite()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition2D, Vector2.zero);
        if (hits.Length > 1)
        {
            return hits.OrderByDescending(i => i.collider.transform.position.z).First(x => x.transform.GetComponent<OverlayTile>() != null);
        }

        return null;
    }
}
