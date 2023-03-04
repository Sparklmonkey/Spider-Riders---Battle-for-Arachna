using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayTile : MonoBehaviour
{
    private SpriteRenderer _overlaySprite;
    private GameObject _itemObject;
    public int G, H;

    public int F { get { return G + H; } }

    public bool IsBlocked { get { return gameObject.name == "NotWalkableTile"; } }
    public OverlayTile Previous;
    public Vector3Int tileLocation;
    private void Awake()
    {
        _overlaySprite = gameObject.GetComponent<SpriteRenderer>();
    }
    public void RenderTileContent()
    {
        switch (gameObject.name)
        {
            case "Rope":
                _itemObject = Instantiate(Resources.Load<GameObject>($"Prefabs/Items/Rope"), transform);
                _itemObject.transform.position = transform.position;
                break;
            case "CardItem":
                _itemObject = Instantiate(Resources.Load<GameObject>($"Prefabs/Items/CardItem"), transform);
                _itemObject.transform.position = transform.position;
                break;
            case "Monacle":
                _itemObject = Instantiate(Resources.Load<GameObject>($"Prefabs/Items/Monacle"), transform);
                _itemObject.transform.position = transform.position;
                break;
            case "Rope_Action_Unused":
                _itemObject = Instantiate(Resources.Load<GameObject>($"Prefabs/Items/Action_Anim"), transform);
                _itemObject.transform.position = transform.position;
                break;
            default:
                break;
        }
    }

    public void PlaceRope()
    {
        if (!TestPlayer<PlayerData>.PlayerHasItem("Rope")) { return; }
        gameObject.name = "Animate_Climb_Up";
        MapManager.Instance.Map[new Vector2Int(tileLocation.x + 4, tileLocation.y + 3)].gameObject.name = "Animate_Climb_Down";
        TestPlayer<PlayerData>.RemoveItemFromInventory(gameObject.name);
        MapSceneManager.Instance.AddRopeUsedImage();
        Destroy(_itemObject);
    }
    public bool PickUpItem()
    {
        if (!MapManager.Instance.ItemList.Contains(gameObject.name)) { return false; }
        TestPlayer<PlayerData>.AddItemToInventory(gameObject.name);
        Destroy(_itemObject);
        gameObject.name = "WalkableTile";
        return true;
    }

    public bool StartBattle()
    {
        return true;
    }

    public bool EndMission()
    {
        return true;
    }
}
