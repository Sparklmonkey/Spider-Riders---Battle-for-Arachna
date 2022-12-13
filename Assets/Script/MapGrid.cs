using System;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour
{

    [SerializeField]
  private int gridHeight, gridWidth;
    [SerializeField]
    private float tileSize;

    public MapTileDisplay[,] gridMap;
    private List<GameObject> tileList = new List<GameObject>();
    [SerializeField]
    private MovementManager movementManager;

    public MapTileDisplay GetPlayerSpawn()
    {
        foreach (var tile in gridMap)
        {
            if(tile == null) { continue; }
            if (tile.isPlayerSpawn) { return tile; }
        }
        return null;
    }

    public MapTileDisplay[,] GenerateGrid()
    {
        gridMap = new MapTileDisplay[gridHeight, gridWidth];

        foreach(Transform child in transform)
        {
            MapTileDisplay mapTileDisplay = child.GetComponent<MapTileDisplay>();
            if(mapTileDisplay == null) { continue; }
            gridMap[mapTileDisplay.X, mapTileDisplay.Y] = mapTileDisplay;
        }
        return gridMap;
    }
}

[Serializable]
public class Item
{
    public string itemName;
}

[Serializable]
public class NPC
{

}

[Serializable]
public class MapScene
{
    public List<GridCoords> walkable;
    public List<Item> items;
    public GridCoords spawnLocation;
    public List<GridCoords> doors;
    public List<NPC> npc;
}

[Serializable]
public class GridCoords
{
    public int x;
    public int y;
}