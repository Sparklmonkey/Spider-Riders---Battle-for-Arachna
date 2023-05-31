using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card", order = 1)]
public class Card : ScriptableObject
{
    [field: SerializeField] public CardType CardType { get; private set; }
    [field: SerializeField] public CardCategories CardCategories { get; private set; }
    [field: SerializeField] public int BuyCost { get; private set; }
    [field: SerializeField] public int SellCost { get; private set; }
    [field: SerializeField] public Sprite CardImage { get; private set; }

    [field: SerializeField]/*[field: UnityEngine.Serialization.FormerlySerializedAs("<StatModifiers>k__BackingField")]*/ public StatModifiers ActivatedOwnerStatModifiers { get; private set; }
    [field: SerializeField] public StatModifiers ActivatedOpponentStatModifiers { get; private set; }
    [field: SerializeField] public int CardsToDraw { get; private set; }
    [field: SerializeField] public int HealAmount { get; private set; }
    [field: SerializeField] public int CardFuseLimit { get; private set; }
    [field: SerializeField] public int TurnsActive { get; private set; }
    [field: SerializeField] public CardEffect OnActivatedCardEffect { get; private set; }
    [field: SerializeField] public CardEffect OnDeactivatedCardEffect { get; private set; }
    [field: SerializeField] public CardEffect OnDroppedOntoCardEffect { get; private set; }

    [field: SerializeField] public string CardName { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
}
