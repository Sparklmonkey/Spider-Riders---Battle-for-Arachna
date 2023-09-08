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
    [SerializeField]
    private PlayerController _playerController;

    [SerializeField]
    private BattleManager _battleManager;
    private Vector2Int _battleTile;
    public bool IsInBattle = false;

    public void SetupMobBattle(string mobName, Vector2Int battleTile)
    {
        _battleManager.gameObject.SetActive(true);
        IsInBattle = true;
        _battleTile = battleTile;
        var mobData = MobDirectory.Instance.GetMobDataWithId(mobName);
        _battleManager.SetupBattleManager(mobData);
    }

    public void SetupMobBattle(string mobName)
    {
        _battleManager.gameObject.SetActive(true);
        IsInBattle = true;
        var mobData = MobDirectory.Instance.GetMobDataWithId(mobName);
        _battleManager.SetupBattleManager(mobData);
    }

    public void BattleVictory()
    {
        //ChangeTileToWalkable(_battleTile);
        _battleManager.gameObject.SetActive(false);

    }

    public Dictionary<Vector2Int, OverlayTile> Map;

    public OverlayTile OverlayTilePrefab;
    private GameObject _gridObject;
    public GameObject OverlayTileContainer;
    private Tilemap _currentTileMap;
    private int _sceneIndex, _previousSceneIndex;
    public OverlayTile GetPlayerStartTile(int origin)
    {
        if(origin == 0)
        {
            return Map.First(x => x.Value.name == "PlayerSpawn").Value;
        }
        else
        {
            return Map.First(x => x.Value.name == $"Spawn_{_previousSceneIndex}").Value;
        }
    }

    public void ChangeTileToWalkable(Vector2Int tile)
    {
        _currentTileMap.SetTile(new Vector3Int(tile.x, tile.y, 0), Resources.Load<TileBase>("TileTypes/Basic/WalkableTile"));
        Map[tile].gameObject.name = "WalkableTile";
    }
    public void ChangeTileToOpenDoor(Vector2Int tile)
    {
        _currentTileMap.SetTile(new Vector3Int(tile.x, tile.y, 0), Resources.Load<TileBase>("TileTypes/Items/Door_Open"));
        Map[tile].gameObject.name = "Door_Open";
    }
    public void ChangeTileToBoardUsed(Vector2Int tile)
    {
        _currentTileMap.SetTile(new Vector3Int(tile.x, tile.y, 0), Resources.Load<TileBase>("TileTypes/Items/Board_Used"));
        Map[tile].gameObject.name = "Board_Used";
    }
    public void ChangeTileRopeUsed(Vector2Int goUp, Vector2Int goDown, bool isRight)
    {
        _currentTileMap.SetTile(new Vector3Int(goUp.x, goUp.y, 0), Resources.Load<TileBase>($"TileTypes/Actions/Animate_Climb_Up_{(isRight ? "Right" : "Left")}"));
        Map[goUp].gameObject.name = $"Animate_Climb_Up_{(isRight ? "Right" : "Left")}";
        _currentTileMap.SetTile(new Vector3Int(goDown.x, goDown.y, 0), Resources.Load<TileBase>($"TileTypes/Actions/Animate_Climb_Down_{(isRight ? "Right" : "Left")}"));
        Map[goDown].gameObject.name = $"Animate_Climb_Down_{(isRight ? "Right" : "Left")}";
    }

    public void SetupNextMission()
    {
        Destroy(_gridObject.gameObject);
        _gridObject = Instantiate(Resources.Load<GameObject>($"Prefabs/TileMaps/Mission_{TestPlayer<PlayerData>.CurrentMissionIndex}/Grid"), transform);
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
        }
    }

    private void ClearOverlayContainer()
    {
        foreach (Transform child in OverlayTileContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }


    public void SetupFirstScene(PlayerController controller)
    {
        _playerController = controller;
        _gridObject = Instantiate(Resources.Load<GameObject>($"Prefabs/TileMaps/Mission_{TestPlayer<PlayerData>.CurrentMissionIndex}/Grid"), transform);
        _sceneList = new List<Tilemap>(_gridObject.GetComponentsInChildren<Tilemap>(true));
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
                    overlayTile.tileLocation = tilePosition;
                    overlayTile.RenderTileContent(_currentTileMap.GetTile(tilePosition).name);
                    overlayTile.OnTileClick += _playerController.StartPathFinding;
                    Map.Add(tileKey, overlayTile);
                }
            }
        }
    }

    public void SetupMap(int scene)
    {
        ClearOverlayContainer();
        _currentTileMap.gameObject.SetActive(false);
        _previousSceneIndex = _sceneIndex + 1;
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
                    overlayTile.tileLocation = tilePosition;
                    overlayTile.RenderTileContent(_currentTileMap.GetTile(tilePosition).name);
                    overlayTile.OnTileClick += _playerController.StartPathFinding;
                    Map.Add(tileKey, overlayTile);
                }
            }
        }
        var playerStartTile = GetPlayerStartTile(_previousSceneIndex);
        _playerController.SetPosition(playerStartTile);
    }



    private readonly List<(int, int)> _missionThreePortal = new() { (0, 1), (1,-1), (1,-2), (0,1), (0,0), (0,-1), (0,-2), (0,-3), (-1,1), (-1,0), (-1,-1),
                                                                    (-1,-2), (-1,-3), (-2,1), (-2,0), (-2,-1), (-2,-2), (-2,-3), (-3,0), (-3,-1), (-3,-2) };
    public void OpenPortal()
    {
        foreach (var item in _missionThreePortal)
        {
            _currentTileMap.SetTile(new Vector3Int(item.Item1, item.Item2, 0), Resources.Load<TileBase>("TileTypes/Basic/MissionEnd"));
            Map[new Vector2Int(item.Item1, item.Item2)].gameObject.name = "MissionEnd";
        }
    }
}
