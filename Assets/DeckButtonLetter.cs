using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckButtonLetter : MonoBehaviour
{
    [SerializeField]
    private Image _deckLetter;
    // Start is called before the first frame update
    void Start()
    {
        _deckLetter.sprite = Resources.Load<Sprite>($"Sprites/Deck{TestPlayer<PlayerData>.GetSelectedDeck()}");
    }

    public void ChangeSelectedDeck()
    {
        switch (TestPlayer<PlayerData>.GetSelectedDeck())
        {
            case "A":
                TestPlayer<PlayerData>.ChangeDeck("B");
                break;
            case "B":
                TestPlayer<PlayerData>.ChangeDeck("C");
                break;
            case "C":
                TestPlayer<PlayerData>.ChangeDeck("A");
                break;
            default:
                break;
        }
        _deckLetter.sprite = Resources.Load<Sprite>($"Sprites/Deck{TestPlayer<PlayerData>.GetSelectedDeck()}");
    }
}
