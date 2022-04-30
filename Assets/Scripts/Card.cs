using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card", order = 1)]
public class Card : ScriptableObject
{
    public CardType cardType;
    public int buyCost;
    public int sellCost;

    public List<StatModifier> statModifyList;

    public string description;
}

[Serializable]
public class StatModifier
{
    public StatType statType;
    public int amount;
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
