using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    public TextMeshProUGUI cardName, cardPrice, cardDesc, atkValue, defValue, diceValue;
    [SerializeField]
    public GameObject atkObject, defObject, diceObject, cardType;
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
        int attackModifier = card.ActivatedOwnerStatModifiers.attackModifier;
        atkValue.text = attackModifier.ToString();
        atkObject.SetActive(attackModifier != 0);
        int defenseModifier = card.ActivatedOwnerStatModifiers.defenseModifier;
        defValue.text = defenseModifier.ToString();
        defObject.SetActive(defenseModifier != 0);
        int diceModifier = card.ActivatedOwnerStatModifiers.diceModifier;
        diceValue.text = diceModifier.ToString();
        diceObject.SetActive(diceModifier != 0);
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
