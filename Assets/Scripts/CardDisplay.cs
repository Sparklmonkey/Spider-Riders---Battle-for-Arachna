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
        cardDesc.text = card.description;
        cardPrice.text = $"{card.buyCost}G";
        cardOnDisplay = card;
        atkObject.SetActive(false);
        defObject.SetActive(false);
        healthObject.SetActive(false);
        cardType.transform.GetChild(0).gameObject.SetActive(false);
        cardType.transform.GetChild(1).gameObject.SetActive(false);
        cardType.transform.GetChild(2).gameObject.SetActive(false);
        if (card.statModifyList != null)
        {
            foreach (StatModifier item in card.statModifyList)
            {
                switch (item.statType)
                {
                    case StatType.Power:
                        atkValue.text = item.amount.ToString();
                        atkObject.SetActive(true);
                        break;
                    case StatType.Defense:
                        defValue.text = item.amount.ToString();
                        defObject.SetActive(true);
                        break;
                    case StatType.Health:
                        healthValue.text = item.amount.ToString();
                        healthObject.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
        }
        switch (card.cardType)
        {
            case CardType.Boost:
                cardType.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case CardType.Equipment:
                cardType.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case CardType.BattleAction:
                cardType.transform.GetChild(2).gameObject.SetActive(true);
                break;
            default:
                break;
        }
        cardImage.sprite = Resources.Load<Sprite>($"Sprites/Cards/Images/{card.cardType}/{card.name}");
        cardBack.sprite = Resources.Load<Sprite>($"Sprites/Cards/Back/{card.cardType}");
    }

    public void CardSelectedPressed(bool isSelected)
    {
        inventoryManager.CardSelectedPressed(isSelected, cardOnDisplay);
    }
}
