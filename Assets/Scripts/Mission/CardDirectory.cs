using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDirectory 
{
    public static CardDirectory Instance
    {
        get
        {
            if (_instance == null) { _instance = new(); }
            return _instance;
        }
    }
    private static CardDirectory _instance;

    public List<Card> directory;

    public CardDirectory()
    {
        directory = new();
        directory.AddRange(Resources.LoadAll<Card>("Cards/BattleAction/"));
        directory.AddRange(Resources.LoadAll<Card>("Cards/Boost/"));
        directory.AddRange(Resources.LoadAll<Card>("Cards/Equipment/"));
    }

    public Card GetCardWithId(string cardId)
    {
        return directory.Find(x => x.CardId == cardId);
    }
}
