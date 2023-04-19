using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card", order = 1)]
public class Card : ScriptableObject
{
    [field: SerializeField] public CardBackgroundType CardType { get; private set; }
    [field: SerializeField] public CardCategories CardCategories { get; private set; }
    [field: SerializeField] public int BuyCost { get; private set; }
    [field: SerializeField] public int SellCost { get; private set; }
    [field: SerializeField] public Sprite CardImage { get; private set; }

    [field: SerializeField]/*[field: UnityEngine.Serialization.FormerlySerializedAs("<StatModifiers>k__BackingField")]*/ public List<StatModifier> ActivatedOwnerStatModifiers { get; private set; }
    [field: SerializeField] public List<StatModifier> ActivatedOpponentStatModifiers { get; private set; }
    [field: SerializeField] public int CardsToDraw { get; private set; }
    [field: SerializeField] public int HealAmount { get; private set; }
    [field: SerializeField] public int TurnsActive { get; private set; }

    [field: SerializeField] public string CardName { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
}

[Serializable]
public struct StatModifier
{
    public StatType statType;
    public int amount;
}

public enum CardBackgroundType
{
    Boost,
    Equipment,
    BattleAction,
}

[Serializable]
public struct CardCategories
{
    public bool isWeapon;
    public bool isArmor;
    public bool isItem;
}

public enum StatType
{
    Attack,
    Defense,
    Dice,
}
