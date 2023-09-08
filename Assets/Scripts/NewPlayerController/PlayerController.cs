using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MovementController _movementController;
    private GameObject _characterObject;

    public GameObject CharacterPrefab;

    private void Start()
    {
        if (_characterObject == null)
        {
            MapManager.Instance.SetupFirstScene(this);
            var playerStartTile = MapManager.Instance.GetPlayerStartTile(0);
            _characterObject = Instantiate(CharacterPrefab);
            _characterObject.transform.position = playerStartTile.transform.position;
            _movementController = new(5f, playerStartTile);
            _movementController.OnCharacterMove += MoveCharacter;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MissionInventoryManager.Instance.IsInventoryOpen) { return; }
        if (MapManager.Instance.IsInBattle) { return; }

        _movementController.MoveAlongPath();
    }

    public void StartPathFinding(OverlayTile destination)
    {
        if (MissionInventoryManager.Instance.IsInventoryOpen) { return; }
        if (MapManager.Instance.IsInBattle) { return; }
        _movementController.StartMovingPlayer(destination);
    }

    public void MoveCharacter(Vector2 destination, float step)
    {
        _characterObject.transform.position = Vector2.MoveTowards(_characterObject.transform.position, destination, step);
        if (Vector2.Distance(_characterObject.transform.position, destination) < 0.0001f)
        {
            _characterObject.transform.position = destination;
            _movementController.ArrivedAtTile();
        }
    }

    public void SetPosition(OverlayTile playerStartTile)
    {
        _movementController.SetCurrentTile(playerStartTile);
        _characterObject.transform.position = playerStartTile.transform.position;
    }
}
