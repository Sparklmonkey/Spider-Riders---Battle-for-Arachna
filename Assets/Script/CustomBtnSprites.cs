using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomBtnSprites : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField]
    private Sprite hoverSprite, clickedSprite, normalSprite;
    private Image buttonImage;
    public void OnPointerDown(PointerEventData eventData)
    {
        if(clickedSprite != null)
        {
            buttonImage.sprite = clickedSprite;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSprite != null)
        {
            buttonImage.sprite = hoverSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite;
    }

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
    }
}
