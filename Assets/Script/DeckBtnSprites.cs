using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeckBtnSprites : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField]
    private Sprite hoverSprite, clickedSprite, normalSprite;
    private Image buttonImage;
    public List<DeckBtnSprites> deckBtns;
    private bool isSelected;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected)
        {
            buttonImage.sprite = hoverSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
        {
            buttonImage.sprite = hoverSprite;
        }
    }

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isSelected = true;
        foreach (DeckBtnSprites item in deckBtns)
        {
            item.DeselectBtn();
        }
        buttonImage.sprite = clickedSprite;
    }

    public void DeselectBtn()
    {
        isSelected = false;
        buttonImage.sprite = normalSprite;
    }
}
