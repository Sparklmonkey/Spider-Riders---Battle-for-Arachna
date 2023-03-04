using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public GameObject CharacterPrefab;
    public float Speed = 5f;
    private CharacterMapInfo _characterObject;

    private PathFinder _pathFinder;
    private List<OverlayTile> _currentPath;
    // Start is called before the first frame update
    void Start()
    {
        _pathFinder = new PathFinder();
        _currentPath = new List<OverlayTile>();

        if (_characterObject == null)
        {
            MapManager.Instance.SetupFirstScene();
            var playerStartTile = MapManager.Instance.GetPlayerStartTile();
            _characterObject = Instantiate(CharacterPrefab).GetComponent<CharacterMapInfo>();
            _characterObject.CurrentTile = playerStartTile;
            _characterObject.transform.position = playerStartTile.transform.position;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var focusedTileHit = GetFocusOnTile();

        if (focusedTileHit.HasValue)
        {
            var overlayTile = focusedTileHit.Value.collider.gameObject;
            transform.position = overlayTile.transform.position;
            GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;

            if (Input.GetMouseButtonDown(0))
            {
                _currentPath = _pathFinder.FindPath(_characterObject.CurrentTile, overlayTile.GetComponent<OverlayTile>());
            }
        }

        if(_currentPath.Count > 0)
        {
            MoveAlongPath();
        }
    }


    private void MoveAlongPath()
    {
        var step = Speed * Time.deltaTime;
        var destination = new Vector2(_currentPath[0].transform.position.x, _currentPath[0].transform.position.y - 0.12f);
        _characterObject.transform.position = Vector2.MoveTowards(_characterObject.transform.position, destination, step);

        if(Vector2.Distance(_characterObject.transform.position, destination) < 0.0001f)
        {
            PositionCharacterOnTile(_currentPath[0]);

            switch (_currentPath[0].gameObject.name)
            {
                case "Transition_1":
                    MapManager.Instance.SetupMap(0);
                    MapSceneManager.Instance.SetupMapScene(1);
                    var playerStartTile = MapManager.Instance.GetPlayerStartTile();
                    _characterObject.CurrentTile = playerStartTile;
                    _characterObject.transform.position = playerStartTile.transform.position;
                    _currentPath.Clear();
                    return;
                case "Transition_2":
                    MapManager.Instance.SetupMap(1);
                    MapSceneManager.Instance.SetupMapScene(2);
                    playerStartTile = MapManager.Instance.GetPlayerStartTile();
                    _characterObject.CurrentTile = playerStartTile;
                    _characterObject.transform.position = playerStartTile.transform.position;
                    _currentPath.Clear();
                    return;
                case "Transition_3":
                    MapManager.Instance.SetupMap(2);
                    MapSceneManager.Instance.SetupMapScene(3);
                    playerStartTile = MapManager.Instance.GetPlayerStartTile();
                    _characterObject.CurrentTile = playerStartTile;
                    _characterObject.transform.position = playerStartTile.transform.position;
                    _currentPath.Clear();
                    return;
                case "MissionEnd":
                    TestPlayer<PlayerData>.FinishMission();
                    MapManager.Instance.SetupNextMission();
                    MapSceneManager.Instance.SetupMapScene(1);
                    playerStartTile = MapManager.Instance.GetPlayerStartTile();
                    _characterObject.CurrentTile = playerStartTile;
                    _characterObject.transform.position = playerStartTile.transform.position;
                    _currentPath.Clear();
                    return;
                case "Animate_Climb_Up":
                    _characterObject.CurrentTile = MapManager.Instance.Map[new Vector2Int(_characterObject.CurrentTile.tileLocation.x + 4, _characterObject.CurrentTile.tileLocation.y + 3)];
                    _characterObject.transform.position = _characterObject.CurrentTile.transform.position;
                    _currentPath.Clear();
                    return;
                case "Animate_Climb_Down":
                    _characterObject.CurrentTile = MapManager.Instance.Map[new Vector2Int(_characterObject.CurrentTile.tileLocation.x - 4, _characterObject.CurrentTile.tileLocation.y - 3)];
                    _characterObject.transform.position = _characterObject.CurrentTile.transform.position;
                    _currentPath.Clear();
                    return;
                case "Rope_Action_Unused":
                    _currentPath[0].PlaceRope();
                    _characterObject.CurrentTile = MapManager.Instance.Map[new Vector2Int(_characterObject.CurrentTile.tileLocation.x + 4, _characterObject.CurrentTile.tileLocation.y + 3)];
                    _characterObject.transform.position = _characterObject.CurrentTile.transform.position;
                    _currentPath.Clear();
                    return;
                default:
                    break;
            }

            if (_currentPath[0].PickUpItem())
            {
                _currentPath.Clear();
            }
            else
            {
                _currentPath.RemoveAt(0);
            }
        }
    }

    private void PositionCharacterOnTile(OverlayTile tile)
    {
        _characterObject.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y -0.12f, tile.transform.position.z);
        _characterObject.CurrentTile = tile;
    }

    public RaycastHit2D? GetFocusOnTile()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition2D, Vector2.zero);
        if (hits.Length > 0)
        {
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }

        return null;
    }

}
