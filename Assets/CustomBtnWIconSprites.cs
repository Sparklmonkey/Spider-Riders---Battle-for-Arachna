using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomBtnWIconSprites : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField]
    private Sprite hoverSprite, clickedSprite, normalSprite, hoverIconSprite, clickedIconSprite, normalIconSprite;
    private Image buttonImage, iconImage;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (clickedSprite != null)
        {
            buttonImage.sprite = clickedSprite;
            iconImage.sprite = clickedIconSprite;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite;
        iconImage.sprite = normalIconSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSprite != null)
        {
            buttonImage.sprite = hoverSprite;
            iconImage.sprite = hoverIconSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite;
        iconImage.sprite = normalIconSprite;
    }

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        iconImage = transform.GetChild(0).GetComponent<Image>();
    }
}
