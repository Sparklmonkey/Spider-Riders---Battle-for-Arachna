using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomBtnWIconSprites : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    [SerializeField]
    private Sprite hoverSprite, clickedSprite, normalSprite, hoverIconSprite, clickedIconSprite, normalIconSprite;
    private Image buttonImage, iconImage;
    private bool isClicked = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (clickedSprite != null)
        {
            ButtonSpriteManager.TurnOffAllButtons();
            isClicked = true;
            buttonImage.sprite = clickedSprite;
            iconImage.sprite = clickedIconSprite;
        }
    }

    public void TurnOffButton()
    {
        isClicked = false;
        buttonImage.sprite = normalSprite;
        iconImage.sprite = normalIconSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSprite != null && !isClicked)
        {
            buttonImage.sprite = hoverSprite;
            iconImage.sprite = hoverIconSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isClicked)
        {
            buttonImage.sprite = normalSprite;
            iconImage.sprite = normalIconSprite;
        }
    }

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        iconImage = transform.GetChild(0).GetComponent<Image>();
    }
}
