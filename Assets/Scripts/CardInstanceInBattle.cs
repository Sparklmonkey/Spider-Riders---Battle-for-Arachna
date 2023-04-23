using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInstanceInBattle
{
    public Card CardDefinition { get; private set; }
    public IBattleParticipant CardOwner { get; set; }
    public IBattleParticipant CardTarget { get; set; }
    public CardSlot CardSlot { get; set; }
    public bool IsActivated { get; set; }
    public int CardsToDraw { get; set; }
    public int ElapsedTurnsActive { get; set; }
    public int UsedFuses { get; set; }
    public List<StatModifier> ActiveOwnerStatModifiers { get; set; }
    public List<StatModifier> ActiveOpponentStatModifiers { get; set; }

    public CardInstanceInBattle(Card cardDefinition, IBattleParticipant cardOwner, CardSlot cardSlot = CardSlot.OnDeck)
    {
        CardDefinition = cardDefinition;
        CardOwner = cardOwner;
        CardTarget = null;
        CardSlot = cardSlot;
        IsActivated = false;
        CardsToDraw = cardDefinition.CardsToDraw;
        ElapsedTurnsActive = 0;
        UsedFuses = 0;
        // I am not sure if there is a better way of doing this, or if this does not work...
        // I am trying to deep copy so that modifying the instance parameters does not affect the
        // cardDefinition.
        ActiveOwnerStatModifiers = new List<StatModifier>();
        ActiveOpponentStatModifiers = new List<StatModifier>();
    }
}

public enum CardSlot
{
    OnDeck,
    Slot1,
    Slot2,
    Slot3,
    Slot4,
    Slot5,
    Slot6,
    Slot7,
    OnCemetery,
}

public interface IBattleParticipant
{
}
