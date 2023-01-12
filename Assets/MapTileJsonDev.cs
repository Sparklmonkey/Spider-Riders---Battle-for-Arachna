using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class MapTileJsonDev : MonoBehaviour
{
    public Image tileImage;

    public string itemName;
    public string npcName;
    public float tileX;
    public float tileY;
    public TileType tileType;
    public string transitionDestination;
    private void Awake()
    {
        tileImage = GetComponent<Image>();
    }

    private void Update()
    {
        UpdateTileColor();
        UpdateTileXY();
    }
    
    private void UpdateTileXY()
    {
        if (tileX == 0)
        {
            var name = gameObject.name.Split(char.Parse("-"));
            tileX = int.Parse(name[0]);
            tileY = int.Parse(name[1]);
        }
    }

    private void UpdateTileColor()
    {
        if (tileImage == null)
        {
            tileImage = GetComponent<Image>();
        }

        switch (tileType)
        {
            case TileType.PlayerSpawn:
                tileImage.color = new Color32(21, 61, 255, 255);
                break;
            case TileType.Transition:
                tileImage.color = new Color32(255, 61, 255, 255);
                break;
            case TileType.Walkable:
                tileImage.color = new Color32(21, 255, 54, 255);
                break;
            case TileType.NPC:
                break;
            case TileType.Item:
                tileImage.color = new Color32(21, 255, 255, 255);
                break;
            default:
                tileImage.color = new Color32(255, 20, 64, 255);
                break;
        }
    }
}
