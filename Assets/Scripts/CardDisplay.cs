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
        cardType.transform.GetChild(0).gameObject.SetActive(false);
        cardType.transform.GetChild(1).gameObject.SetActive(false);
        cardType.transform.GetChild(2).gameObject.SetActive(false);
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
                    //case StatType.Health:
                    //    healthValue.text = item.amount.ToString();
                    //    healthObject.SetActive(true);
                    //    break;
                    default:
                        break;
                }
            }
        }
        switch (card.CardType)
        {
            case CardBackgroundType.Boost:
                cardType.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case CardBackgroundType.Equipment:
                cardType.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case CardBackgroundType.BattleAction:
                cardType.transform.GetChild(2).gameObject.SetActive(true);
                break;
            default:
                break;
        }
        cardImage.sprite = Resources.Load<Sprite>($"Sprites/Cards/Images/{card.CardType}/{card.name}");
        cardBack.sprite = Resources.Load<Sprite>($"Sprites/Cards/Back/{card.CardType}");
    }

    public void CardSelectedPressed(bool isSelected)
    {
        inventoryManager.CardSelectedPressed(isSelected, cardOnDisplay);
    }
}
