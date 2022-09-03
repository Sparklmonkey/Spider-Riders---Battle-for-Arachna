using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UseRopeAction : MonoBehaviour, IActionItem
{
    [SerializeField]
    private Image ropeUsedImage;
    [SerializeField]
    private List<MapTileDisplay> tilesToChange;
    private bool isActive;
    [SerializeField]
    private GameObject animationObject;

    public void ActionActivate()
    {
        if (TestPlayer<PlayerData>.PlayerHasItem("Rope") && !isActive)
        {
            Destroy(animationObject);
            isActive = true;
            ropeUsedImage.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
            foreach (var item in tilesToChange)
            {
                item.isWalkable = true;
            }
        }
    }
}
