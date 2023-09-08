using System;
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

    public event Action<OverlayTile> OnTileClick;

    private void Awake()
    {
        _overlaySprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void DestroyChildObject()
    {
        Destroy(_itemObject);
    }

    public void RenderTileContent(string tileName)
    {
        name = tileName;

        var scriptName = name.Split("_");

        var scriptObject = Type.GetType($"{scriptName[0]}Interactable");
        if(scriptObject == null) { return; }

        gameObject.AddComponent(scriptObject);

        //if (name.Contains("Transition_"))
        //{
            //gameObject.AddComponent<TransitionInteractable>();
        //}

        if (name.Contains("Item_"))
        {
            _itemObject = Instantiate(Resources.Load<GameObject>($"Prefabs/Items/{gameObject.name.Replace("Item_", "")}"), transform);
            _itemObject.transform.position = transform.position;
            //gameObject.AddComponent<ItemInteractable>();
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

    private void OnMouseDown()
    {
        OnTileClick?.Invoke(this);
    }

    private void OnMouseUp()
    {
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
