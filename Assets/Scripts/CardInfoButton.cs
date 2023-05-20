using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
public class CardInfoButton : MonoBehaviour
{
    [SerializeField] private List<Sprite> spriteList;
    [SerializeField] private SpriteRenderer buttonRenderer;
    [SerializeField] private CardInPlay cardInPlay;

    private Card CardDefinition
    {
        get
        {
            if (cardInPlay != null && cardInPlay.Reference != null) return cardInPlay.Reference.CardDefinition;
            return null;
        }
    }
    private IBattleManager BattleManager
    {
        get
        {
            if (cardInPlay != null && cardInPlay.Reference != null) return cardInPlay.Reference.BattleManager;
            return null;
        }
    }

    private void OnMouseDown()
    {
        buttonRenderer.sprite = spriteList[0];
        Debug.LogWarning($"Please fix CardInfoButton by importing its assets instead of using a purple circle");
    }
    private void OnMouseUp()
    {
        buttonRenderer.sprite = spriteList[0];
        Debug.LogWarning($"Please fix CardInfoButton by importing its assets instead of using a purple circle");
    }
    private void OnMouseUpAsButton()
    {
        if (BattleManager == null || CardDefinition == null) return;
        BattleManager.OpenCardDetailsPopup(CardDefinition);
    }
    private void OnMouseEnter()
    {
        buttonRenderer.sprite = spriteList[0];
        Debug.LogWarning($"Please fix CardInfoButton by importing its assets instead of using a purple circle");
        // Refer to DefineButton2 (2867) in battleSystem_2.swf
    }
    private void OnMouseExit()
    {
        buttonRenderer.sprite = spriteList[0];
        Debug.LogWarning($"Please fix CardInfoButton by importing its assets instead of using a purple circle");
    }
}
