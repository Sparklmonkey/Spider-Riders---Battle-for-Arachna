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

            case "Door_Open":
                MapSceneManager.Instance.ChangeDoorToOpen();
                break;
            case "Door_Closed":
                MapSceneManager.Instance.AddClosedDoorImage();
                _itemObject = Instantiate(Resources.Load<GameObject>($"Prefabs/Items/Action_Anim"), transform);
                _itemObject.transform.position = transform.position;
                break;
            case "Rope":
            case "CardItem":
            case "Manacle":
            case "Statue":
            case "Key":
            case "Board_Overworld":
                _itemObject = Instantiate(Resources.Load<GameObject>($"Prefabs/Items/{gameObject.name}"), transform);
                _itemObject.transform.position = transform.position;
                break;
            case "Action_Rope_Unused_Right":
            case "Action_Rope_Unused_Left":
            case "Action_Board_Unused":
                _itemObject = Instantiate(Resources.Load<GameObject>($"Prefabs/Items/Action_Anim"), transform);
                _itemObject.transform.position = transform.position;
                break;
            case "Animate_Climb_Up_Right":
            case "Animate_Climb_Up_Left":
                MapSceneManager.Instance.AddRopeUsedImage();
                break;
            default:
                break;
        }
    }

    public bool GiveManacle()
    {
        if (!TestPlayer<PlayerData>.PlayerHasItem("Manacle")) { return false; }
        TestPlayer<PlayerData>.RemoveItemFromInventory(gameObject.name);
        MapSceneManager.Instance.OpenManaclePortal();
        Destroy(_itemObject);
        return true;
    }
    public bool PlaceRope()
    {
        if (!TestPlayer<PlayerData>.PlayerHasItem("Rope")) { return false; }
        TestPlayer<PlayerData>.RemoveItemFromInventory(gameObject.name);
        MapSceneManager.Instance.AddRopeUsedImage();
        Destroy(_itemObject);
        return true;
    }
    public bool PlaceBoard()
    {
        if (!TestPlayer<PlayerData>.PlayerHasItem("Board_Overworld")) { return false; }
        TestPlayer<PlayerData>.RemoveItemFromInventory(gameObject.name);
        MapSceneManager.Instance.AddBoardUsedImage();
        Destroy(_itemObject);
        return true;
    }
    public bool OpenDoor()
    {
        if (!TestPlayer<PlayerData>.PlayerHasItem("Key")) { return false; }
        TestPlayer<PlayerData>.RemoveItemFromInventory(gameObject.name);
        MapSceneManager.Instance.ChangeDoorToOpen();
        Destroy(_itemObject);
        return true;
    }
    public void PickUpItem()
    {
        TestPlayer<PlayerData>.AddItemToInventory(gameObject.name);
        Destroy(_itemObject);
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
