using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance
    {
        get { return _instance; }
    }

    [SerializeField]
    private List<Tilemap> _sceneList;

    public List<string> ItemList = new List<string> { "Rope", "CardItem", "Monacle" };
    public Dictionary<Vector2Int, OverlayTile> Map;

    public OverlayTile OverlayTilePrefab;
    private GameObject _gridObject;
    public GameObject OverlayTileContainer;
    private Tilemap _currentTileMap;
    private int _sceneIndex;
    public OverlayTile GetPlayerStartTile()
    {
        return Map.First(x => x.Value.name == "PlayerSpawn").Value;
    }

    public void SetupNextMission()
    {
        Destroy(_gridObject.gameObject);
        _gridObject = Instantiate(Resources.Load<GameObject>($"Prefabs/TileMaps/Mission_{TestPlayer<PlayerData>.GetPlayerMission()}/Grid"), transform);
        _sceneList = new List<Tilemap>(_gridObject.GetComponentsInChildren<Tilemap>(true));
        SetupMap(0);
    }

    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            _gridObject = GetComponentInChildren<Grid>().gameObject;
            _sceneList = new List<Tilemap>(_gridObject.GetComponentsInChildren<Tilemap>(true));
        }
    }

    private void ClearOverlayContainer()
    {
        foreach (Transform child in OverlayTileContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }


    public void SetupFirstScene()
    {
        _sceneIndex = 0;
        MapSceneManager.Instance.SetupMapScene(1);
        _currentTileMap = _sceneList[_sceneIndex];
        _currentTileMap.gameObject.SetActive(true);
        BoundsInt bounds = _currentTileMap.cellBounds;
        Map = new Dictionary<Vector2Int, OverlayTile>();
        var sortingOrder = _currentTileMap.GetComponent<TilemapRenderer>().sortingOrder;

        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                var tilePosition = new Vector3Int(x, y, 0);
                var tileKey = new Vector2Int(x, y);
                if (_currentTileMap.HasTile(tilePosition) && !Map.ContainsKey(tileKey))
                {
                    var overlayTile = Instantiate(OverlayTilePrefab, OverlayTileContainer.transform);
                    var cellWorldPosition = _currentTileMap.GetCellCenterWorld(tilePosition);

                    overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y - 0.03f, 0);
                    overlayTile.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
                    overlayTile.name = _currentTileMap.GetTile(tilePosition).name;
                    overlayTile.tileLocation = tilePosition;
                    overlayTile.RenderTileContent();
                    Map.Add(tileKey, overlayTile);
                }
            }
        }
    }

    public void SetupMap(int scene)
    {
        ClearOverlayContainer();
        _currentTileMap.gameObject.SetActive(false);
        _sceneIndex = scene;

        _currentTileMap = _sceneList[_sceneIndex];
        _currentTileMap.gameObject.SetActive(true);

        BoundsInt bounds = _currentTileMap.cellBounds;
        Map = new Dictionary<Vector2Int, OverlayTile>();
        var sortingOrder = _currentTileMap.GetComponent<TilemapRenderer>().sortingOrder;

        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                var tilePosition = new Vector3Int(x, y, 0);
                var tileKey = new Vector2Int(x, y);
                if (_currentTileMap.HasTile(tilePosition) && !Map.ContainsKey(tileKey))
                {
                    var overlayTile = Instantiate(OverlayTilePrefab, OverlayTileContainer.transform);
                    var cellWorldPosition = _currentTileMap.GetCellCenterWorld(tilePosition);

                    overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y - 0.03f, 0);
                    overlayTile.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
                    overlayTile.name = _currentTileMap.GetTile(tilePosition).name;
                    overlayTile.tileLocation = tilePosition;
                    overlayTile.RenderTileContent();
                    Map.Add(tileKey, overlayTile);
                }
            }
        }
    }

}
