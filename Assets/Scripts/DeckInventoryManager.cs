using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckInventoryManager : MonoBehaviour
{
    public PlayerData testPlayerData;

    public List<CardDisplay> cardDisplayList;
    public List<CardDisplayButtonManager> cardDisplayBtnList;

    public TextMeshProUGUI pageCountLabel;

    private List<Card> currentDeck;
    private int pageCount = 1;
    private int maxPageCount = 0;
    private int selectedDeckIndex = 0;
    private void Awake()
    {

        pageCountLabel.text = $"{pageCount}";
        maxPageCount = (testPlayerData.cardInventory.Count / 10) + 1;
        currentDeck = testPlayerData.deckA;
        UpdateCardDisplay();
    }

    public void ChangeDeck(int deckSelected)
    {
        switch (selectedDeckIndex)
        {
            case 0:
                testPlayerData.deckA = currentDeck;
                break;
            case 1:
                testPlayerData.deckB = currentDeck;
                break;
            case 2:
                testPlayerData.deckC = currentDeck;
                break;
            default:
                break;
        }
        selectedDeckIndex = deckSelected;
        pageCount = 1;
        switch (deckSelected)
        {
            case 0:
                currentDeck = testPlayerData.deckA;
                break;
            case 1:
                currentDeck = testPlayerData.deckB;
                break;
            case 2:
                currentDeck = testPlayerData.deckC;
                break;
            default:
                break;
        }
        UpdateCardDisplay();
    }

    public void ChangePage(bool isNext)
    {
        if (isNext)
        {
            if (pageCount < maxPageCount)
            {
                pageCount++;
                UpdateCardDisplay();
            }
        }
        else
        {
            if (pageCount > 1)
            {
                pageCount--;
                UpdateCardDisplay();
            }
        }
        pageCountLabel.text = $"{pageCount}";
    }

    private void UpdateCardDisplay()
    {
        int minIndex = ((pageCount - 1) * 10);
        int maxIndex = minIndex + 10;
        int cardDisplayIndex = 0;
        for (int i = minIndex; i < maxIndex; i++)
        {
            if(i < testPlayerData.cardInventory.Count)
            {
                cardDisplayList[cardDisplayIndex].transform.parent.gameObject.SetActive(true);
                cardDisplayList[cardDisplayIndex].SetupCardDisplay(testPlayerData.cardInventory[i]);
                cardDisplayList[cardDisplayIndex].selectCardBtn.SetSelectStatus(currentDeck.Contains(testPlayerData.cardInventory[i]));
            }
            else
            {
                cardDisplayList[cardDisplayIndex].transform.parent.gameObject.SetActive(false);
            }
            cardDisplayIndex++;
        }
    }

    public void CardSelectedPressed(bool isSelected, Card card)
    {
        if (isSelected)
        {
            currentDeck.Add(card);
        }
        else
        {
            currentDeck.Remove(card);
        }
    }
}
