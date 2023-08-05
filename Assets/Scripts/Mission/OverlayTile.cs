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

        if (name.Contains("Item_"))
        {
            _itemObject = Instantiate(Resources.Load<GameObject>($"Prefabs/Items/{gameObject.name.Replace("Item_", "")}"), transform);
            _itemObject.transform.position = transform.position;
        }

        if(name == "Action_Manacle")
        {
            _itemObject = Instantiate(Resources.Load<GameObject>("Prefabs/Items/Statue"), transform);
            _itemObject.transform.position = transform.position;
        }

        if (name.Contains("Action_"))
        {
            _itemObject = Instantiate(Resources.Load<GameObject>("Prefabs/Items/Action_Anim"), transform);
            _itemObject.transform.position = transform.position;
        }

        if (name.Contains("Battle_"))
        {
            _itemObject = Instantiate(Resources.Load<GameObject>("Sprites/MobOverworld/Mob"), transform);
            _itemObject.transform.position = transform.position;
        }

        switch (tileName)
        {
            case "Door_Open":
                MapSceneManager.Instance.ChangeDoorToOpen();
                break;
            case "Board_Used":
                MapSceneManager.Instance.AddBoardUsedImage();
                break;
            case "Animate_ClimbUp_Right":
            case "Animate_ClimbUp_Left":
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
        TestPlayer<PlayerData>.RemoveItemFromInventory("Board");
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
        TestPlayer<PlayerData>.AddItemToInventory(gameObject.name.Replace("Item_", ""));
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
