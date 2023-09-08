using UnityEngine;

public class MobInteractable : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        string enemyName = gameObject.name.Replace("Battle_", "");
        var tile = GetComponentInParent<OverlayTile>();
        MapManager.Instance.ChangeTileToWalkable(new Vector2Int(tile.tileLocation.x, tile.tileLocation.y));
        MapManager.Instance.SetupMobBattle(enemyName);
        Destroy(transform.GetChild(0).gameObject);
        Destroy(this);
    }
}
