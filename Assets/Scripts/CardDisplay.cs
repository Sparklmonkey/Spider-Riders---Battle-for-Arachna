using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    public TextMeshProUGUI cardName, cardPrice, cardDesc, atkValue, defValue, healthValue;
    [SerializeField]
    public GameObject atkObject, defObject, healthObject, cardType;
    [SerializeField]
    public Image cardImage, cardBack;
    [SerializeField]
    public CardDisplay largeDisplay;
    [SerializeField]
    private DeckInventoryManager inventoryManager;
    public SelectCardBtn selectCardBtn;
    private Card cardOnDisplay;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(largeDisplay != null)
        {
            largeDisplay.gameObject.SetActive(true);
            largeDisplay.SetupCardDisplay(cardOnDisplay);
        }
    }

    public void SetupCardDisplay(Card card)
    {
        cardName.text = card.name;
        cardDesc.text = card.Description;
        cardPrice.text = $"{card.BuyCost}G";
        cardOnDisplay = card;
        atkObject.SetActive(false);
        defObject.SetActive(false);
        healthObject.SetActive(false);
        if (card.ActivatedOwnerStatModifiers != null)
        {
            foreach (StatModifier item in card.ActivatedOwnerStatModifiers)
            {
                switch (item.statType)
                {
                    case StatType.Attack:
                        atkValue.text = item.amount.ToString();
                        atkObject.SetActive(true);
                        break;
                    case StatType.Defense:
                        defValue.text = item.amount.ToString();
                        defObject.SetActive(true);
                        break;
                    case StatType.Dice:
                        healthValue.text = item.amount.ToString();
                        healthObject.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
        }
        if (card.CardType != null)
        {
            foreach (Transform child in cardType.transform)
            {
                child.gameObject.SetActive(false);
                if (card.CardType.StylizedTypeTextSprite == child.GetComponent<Image>().sprite)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
        cardImage.sprite = card.CardImage;
        cardBack.sprite = card.CardType.BackgroundSprite;
    }

    public void CardSelectedPressed(bool isSelected)
    {
        inventoryManager.CardSelectedPressed(isSelected, cardOnDisplay);
    }
}
