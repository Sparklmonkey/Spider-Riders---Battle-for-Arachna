
using System;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour
{
[SerializeField]
  private int gridHeight, gridWidth;
    [SerializeField]
    private float tileSize;
  [SerializeField]
  private GameObject tile, playerPrefab;

    private TileData[,] grid;
    private List<GameObject> tileList = new List<GameObject>();

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        string filePath = "Json/MapOne";

        TextAsset targetFile = Resources.Load<TextAsset>(filePath);
        MapScene map = JsonUtility.FromJson<MapScene>(targetFile.text);

        grid = new TileData[gridHeight, gridWidth];
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                float posX = ((x * tileSize + y * tileSize) / 2f) - 968.5f;
                float posY = ((x * tileSize - y * tileSize) / 4f) + 473.5f;
                GameObject newTile = Instantiate(tile, transform);
                newTile.transform.position = new Vector2(posX, posY);

                if (newTile.transform.localPosition.x <= -681.5f || newTile.transform.localPosition.x >= 710.5f || newTile.transform.localPosition.y <= -500.5f || newTile.transform.localPosition.y >= 514.5f)
                {
                    Destroy(newTile.gameObject);
                    grid[x, y] = new TileData();
                    grid[x, y].isWalkable = false;
                }
                else
                {
                    tileList.Add(newTile);
                    newTile.name = $"{x}-{y}";
                    newTile.GetComponent<MapTileDisplay>().SetName($"{x}-{y}");
                    grid[x, y] = newTile.GetComponent<MapTileDisplay>().tileData;
                }

            }
        }

        for (int i = 0; i < map.walkable.Count; i++)
        {
            Debug.Log($"Walkable {i}");
            Debug.Log($"Walkable {map.walkable[i].x} - {map.walkable[i].y}");
            grid[map.walkable[i].x, map.walkable[i].y].isWalkable = true;
        }

        GameObject player = Instantiate(playerPrefab, transform);
        player.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        player.transform.position = grid[map.spawnLocation.x, map.spawnLocation.y].position;
    }
}

[Serializable]
public class Item
{
    public GridCoords position;
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