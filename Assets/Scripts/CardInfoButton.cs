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
        get => cardInPlay?.CardDefinition;
    }
    private IBattleManager BattleManager
    {
        get => cardInPlay?.BattleManager;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        buttonImage.sprite = spriteList?[0];
        Debug.LogWarning($"CardInfoButton of {cardInPlay} is requesting for {BattleManager} to open card info popup containing {CardDefinition} (This feature is not yet implemented)");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = spriteList?[0];
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = spriteList?[0];
    }
}
