using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card", order = 1)]
public class Card : ScriptableObject
{
    public string cardName;
    public CardType cardType;
    public int buyCost;
    public int sellCost;

    public List<StatType> statModifyList;
    public List<int> statModifyAmountList;

    public string description;
}

public enum CardType
{
    Boost,
    Equipment,
    BattleAction
}

public enum StatType
{
    Power,      //Dice
    Defense,    //Defense
    Health      //Health
}
