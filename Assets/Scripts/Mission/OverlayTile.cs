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
    public void RenderTileContent(string tileName)
    {
        name = tileName;
        switch (tileName)
        {
            case "Door_Open":
                MapSceneManager.Instance.ChangeDoorToOpen();
                break;
            case "Door_Closed":
                MapSceneManager.Instance.AddClosedDoorImage();
                _itemObject = Instantiate(Resources.Load<GameObject>($"Prefabs/Items/Action_Anim"), transform);
                _itemObject.transform.position = transform.position;
                break;

            case "Board_Used":
                MapSceneManager.Instance.AddBoardUsedImage();
                break;
            case "Rope":
            case "Card_Item":
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
        TestPlayer<PlayerData>.RemoveItemFromInventory("Manacle");
        MapSceneManager.Instance.OpenManaclePortal();
        Destroy(_itemObject);
        return true;
    }
    public void PlaceRope()
    {
        TestPlayer<PlayerData>.RemoveItemFromInventory("Rope");
        MapSceneManager.Instance.AddRopeUsedImage();
        Destroy(_itemObject);
    }
    public void PlaceBoard()
    {
        TestPlayer<PlayerData>.RemoveItemFromInventory("Board_Overworld");
        MapSceneManager.Instance.AddBoardUsedImage();
        Destroy(_itemObject);
    }
    public void OpenDoor()
    {
        TestPlayer<PlayerData>.RemoveItemFromInventory("Key");
        MapSceneManager.Instance.ChangeDoorToOpen();
        Destroy(_itemObject);
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
