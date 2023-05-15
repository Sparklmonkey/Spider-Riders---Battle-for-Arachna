using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardInfoButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private List<Sprite> spriteList;
    [SerializeField] private Image buttonImage;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        buttonImage.sprite = spriteList[0];
        Debug.LogWarning($"CardInfoButton of {cardInPlay} is requesting for {BattleManager} to open card info popup containing {CardDefinition} (This feature is not yet implemented)");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = spriteList[0];
        Debug.LogWarning($"Please fix CardInfoButton by importing its assets instead of using a purple circle");
        // Refer to DefineButton2 (2867) in battleSystem_2.swf
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = spriteList[0];
    }
}
