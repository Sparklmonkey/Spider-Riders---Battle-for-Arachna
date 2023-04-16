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
    public Sprite image;

    public List<StatModifier> statModifyList;

    public string cardName;
    public string description;
}

[Serializable]
public struct StatModifier
{
    public StatType statType;
    public int amount;
}

public enum CardType
{
    Boost,
    Equipment,
    BattleAction,
}

public enum StatType
{
    Power, //Referred to as Dice elsewhere
    Defense,
    Health, //Referred to as Action in some places in Flash source code
}
