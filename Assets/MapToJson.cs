using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToJson : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public class MapJson
{
    public string mainMapImage;
    public List<MapDetailImage> mapDetailImages;
    public List<TileData> tileDataList;
}

[Serializable]
public class MapDetailImage
{
    public string imageName;
    public int x;
    public int y;
}

[Serializable]
public class TileData
{
    public string itemName;
    public string npcName;
    public float tileX;
    public float tileY;
    public bool isWalkable;
    public bool isPlayerSpawn;
    public bool isTransition;
    public string transitionDestination;
}

[Serializable]
public enum TileType
{
    PlayerSpawn,
    Transition,
    Walkable,
    NPC,
    Item,
    NotWalkable
}