using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MapTileDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image tileDisplay;
    [SerializeField]
    private TextMeshProUGUI tileName;

    public TileData tileData;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tileData.isWalkable)
        {
            tileDisplay.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tileDisplay.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MinValue);
    }
    
    public void SetName(string name)
    {
        tileName.text = name;
        tileData = new TileData();
        tileData.position = new Vector2(transform.position.x, transform.position.y);
    }
}


public class TileData
{
    public bool isWalkable = false;
    public Item item;
    public NPC npc;
    public Vector2 position;
}

