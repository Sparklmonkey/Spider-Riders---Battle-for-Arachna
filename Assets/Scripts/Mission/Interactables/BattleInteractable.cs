using UnityEngine;

public class BattleInteractable : MonoBehaviour, IInteractable
{
    private GameObject _childObject;

    public void OnInteract()
    {
        string enemyName = gameObject.name.Replace("Battle_", "");
        var tile = GetComponentInParent<OverlayTile>();
        MapManager.Instance.ChangeTileToWalkable(new Vector2Int(tile.tileLocation.x, tile.tileLocation.y));
        MapManager.Instance.SetupMobBattle(enemyName);
        Destroy(_childObject);
        Destroy(this);
    }

    public void OnUseItem(string item)
    {
        return;
    }

    public void Setup()
    {
        _childObject = Instantiate(Resources.Load<GameObject>("Sprites/MobOverworld/Mob"), transform);
        _childObject.name = gameObject.name.Replace("Battle_", "");
        var mobAnimator = _childObject.AddComponent<MobAnimationManager>();
        mobAnimator.SetupManager(MobDirectory.Instance.GetMobDataWithId($"{gameObject.name.Replace("Battle_", "")}").mobName);
        _childObject.transform.position = transform.position;
    }
}
