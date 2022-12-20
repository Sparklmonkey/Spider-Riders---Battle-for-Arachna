using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectCardBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private List<Sprite> spriteList;
    [SerializeField]
    private Image selfBtn;
    [SerializeField]
    private CardDisplay cardDisplay;
    private bool isSelected;

    public void SetSelectStatus(bool isSelected)
    {
        this.isSelected = isSelected;
        selfBtn.sprite = spriteList[isSelected ? 2 : 0];
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        isSelected = !isSelected;
        selfBtn.sprite = spriteList[isSelected ? 3 : 5];
        cardDisplay.CardSelectedPressed(isSelected);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selfBtn.sprite = spriteList[isSelected ? 1 : 4];
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selfBtn.sprite = spriteList[isSelected ? 2 : 0];
    }

}
