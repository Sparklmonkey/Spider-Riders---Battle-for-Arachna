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
            case SelectedDeck.A:
                TestPlayer<PlayerData>.ChangeDeck(SelectedDeck.B);
                break;
            case SelectedDeck.B:
                TestPlayer<PlayerData>.ChangeDeck(SelectedDeck.C);
                break;
            case SelectedDeck.C:
                TestPlayer<PlayerData>.ChangeDeck(SelectedDeck.A);
                break;
            default:
                break;
        }
        _deckLetter.sprite = Resources.Load<Sprite>($"Sprites/Deck{TestPlayer<PlayerData>.GetSelectedDeck()}");
    }
}
