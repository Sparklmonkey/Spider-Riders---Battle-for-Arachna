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
            //MapManager.Instance.SetupFirstScene();
            var playerStartTile = MapManager.Instance.GetPlayerStartTile(0);
            _characterObject = Instantiate(CharacterPrefab).GetComponent<CharacterMapInfo>();
            _characterObject.CurrentTile = playerStartTile;
            _characterObject.transform.position = playerStartTile.transform.position;
        }
    }

    public void StartPathFinding(OverlayTile destination)
    {
        _currentPath = _pathFinder.FindPath(_characterObject.CurrentTile, destination);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (MissionInventoryManager.Instance.IsInventoryOpen) { return; }
        if (MapManager.Instance.IsInBattle) { return; }

        if(_currentPath.Count > 0)
        {
            MoveAlongPath();
        }
        else
        {
            if (_runTriggered)
            {
                _characterObject.StopRunAnim();
                _runTriggered = false;
            }
        }
    }

    public static void UseItemFromInventory(string item)
    {
        var focusedTileHit = GetFocusOnTileUnderSprite();

        if (focusedTileHit.HasValue)
        {
            var overlayTile = focusedTileHit.Value.collider.gameObject;
            var tileScript = overlayTile.GetComponent<OverlayTile>();
            switch (item)
            {
                case "Rope":
                    if(overlayTile.name == "Action_Rope_Right")
                    {
                        tileScript.PlaceRope();
                        MapManager.Instance.ChangeTileRopeUsed(new Vector2Int(tileScript.tileLocation.x, tileScript.tileLocation.y),
                                        new Vector2Int(tileScript.tileLocation.x + 4, tileScript.tileLocation.y + 3), true);
                    }
                    else if(overlayTile.name == "Action_Rope_Left")
                    {
                        tileScript.PlaceRope();
                        MapManager.Instance.ChangeTileRopeUsed(new Vector2Int(tileScript.tileLocation.x, tileScript.tileLocation.y), 
                                        new Vector2Int(tileScript.tileLocation.x - 4, tileScript.tileLocation.y - 4), false);
                    }
                    break;
                case "Key":
                    if (overlayTile.name == "Action_Door")
                    {
                        tileScript.OpenDoor();
                        MapManager.Instance.ChangeTileToOpenDoor(new Vector2Int(tileScript.tileLocation.x, tileScript.tileLocation.y));
                        MapManager.Instance.ChangeTileToWalkable(new Vector2Int(tileScript.tileLocation.x, tileScript.tileLocation.y + 1));
                    }
                    break;
                case "Board":
                    if(overlayTile.name == "Action_Board")
                    {
                        tileScript.PlaceBoard();
                        MapManager.Instance.ChangeTileToWalkable(new Vector2Int(tileScript.tileLocation.x - 1, tileScript.tileLocation.y));
                        MapManager.Instance.ChangeTileToWalkable(new Vector2Int(tileScript.tileLocation.x - 2, tileScript.tileLocation.y));
                        MapManager.Instance.ChangeTileToWalkable(new Vector2Int(tileScript.tileLocation.x - 3, tileScript.tileLocation.y));
                        MapManager.Instance.ChangeTileToBoardUsed(new Vector2Int(tileScript.tileLocation.x, tileScript.tileLocation.y));
                    }
                    return;
                case "Manacle":
                    if(overlayTile.name == "Statue")
                    {
                        tileScript.GiveManacle();
                        MapManager.Instance.OpenPortal();
                        MapManager.Instance.ChangeTileToWalkable(new Vector2Int(tileScript.tileLocation.x, tileScript.tileLocation.y + 1));
                    }
                    return;
                default:
                    break;
            }
        }
    }
    private bool _runTriggered;
    private void MoveAlongPath()
    {
        if (!_runTriggered)
        {
            _characterObject.TriggerRunAnim();
            _runTriggered = true;
        }
        var step = Speed * Time.deltaTime;
        var destination = new Vector2(_currentPath[0].transform.position.x, _currentPath[0].transform.position.y - 0.12f);
        _characterObject.transform.position = Vector2.MoveTowards(_characterObject.transform.position, destination, step);

        if(Vector2.Distance(_characterObject.transform.position, destination) < 0.0001f)
        {
            PositionCharacterOnTile(_currentPath[0]);

            if (_currentPath[0].gameObject.name.Contains("Item_"))
            {
                _currentPath[0].PickUpItem();
                _currentPath.Clear();
                MapManager.Instance.ChangeTileToWalkable(new Vector2Int(_characterObject.CurrentTile.tileLocation.x, _characterObject.CurrentTile.tileLocation.y));
                return;
            }



                switch (_currentPath[0].gameObject.name)
            {
                case "Animate_Climb_Up_Left":
                    _characterObject.CurrentTile = MapManager.Instance.Map[new Vector2Int(_characterObject.CurrentTile.tileLocation.x - 4, _characterObject.CurrentTile.tileLocation.y - 4)];
                    _characterObject.transform.position = _characterObject.CurrentTile.transform.position;
                    _currentPath.Clear();
                    return;
                case "Animate_Climb_Down_Left":
                    _characterObject.CurrentTile = MapManager.Instance.Map[new Vector2Int(_characterObject.CurrentTile.tileLocation.x + 4, _characterObject.CurrentTile.tileLocation.y + 4)];
                    _characterObject.transform.position = _characterObject.CurrentTile.transform.position;
                    _currentPath.Clear();
                    return;
                case "Animate_Climb_Up_Right":
                    _characterObject.CurrentTile = MapManager.Instance.Map[new Vector2Int(_characterObject.CurrentTile.tileLocation.x + 4, _characterObject.CurrentTile.tileLocation.y + 3)];
                    _characterObject.transform.position = _characterObject.CurrentTile.transform.position;
                    _currentPath.Clear();
                    return;
                case "Animate_Climb_Down_Right":
                    _characterObject.CurrentTile = MapManager.Instance.Map[new Vector2Int(_characterObject.CurrentTile.tileLocation.x - 4, _characterObject.CurrentTile.tileLocation.y - 3)];
                    _characterObject.transform.position = _characterObject.CurrentTile.transform.position;
                    _currentPath.Clear();
                    return;
                default:
                    break;
            }
            
            _currentPath.RemoveAt(0);
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


    public static RaycastHit2D? GetFocusOnTileUnderSprite()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition2D, Vector2.zero);
        if (hits.Length > 1)
        {
            return hits.OrderByDescending(i => i.collider.transform.position.z).First(x => x.transform.GetComponent<OverlayTile>() != null);
        }

        return null;
    }
}
