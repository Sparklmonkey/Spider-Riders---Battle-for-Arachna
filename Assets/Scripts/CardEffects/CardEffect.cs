using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class CardEffect : ScriptableObject
{
    //public abstract void Subscribe();
    //public abstract void Unsubscribe();
    public abstract void Invoke(BattleManager battleManager);

    protected void HealToFull(bool applyToOpponent = false)
    {
        // return if health is already >= max
        // heal up to full health
    }
    protected void Heal(int healAmount, bool applyToOpponent = false)
    {
        // heal by the specified amount
    }
    protected void HealCapped(int healAmount, bool applyToOpponent = false)
    {
        // heal by the specified amount, but don't go over max health
    }
    protected void DestroyCard(CardBackgroundType cardType, bool applyToOpponent = true)
    {
        // return if there is no card of the specified type in the hand
        // if AI player, automatically select a card to destroy and destroy it
        // if human player, pop up destruction UI keyed to the cardType
    }
    protected void DestroyCard(CardCategories permissibleCardCategories, bool applyToOpponent = true)
    {
        // return if there is no card of the specified type in the hand
        // if AI player, automatically select a card to destroy and destroy it
        // if human player, pop up destruction UI keyed to the permissibleCardCategories
    }
    protected void ApplyStatModifier(StatModifier statModifier, int durationTurns, bool applyToOpponent = false)
    {
        // apply the specified stat modifier
    }
    protected void RollNewDice()
    {
        // remove unused dice and roll new dice
    }
    protected void RollSomeDice(int quantity)
    {
        // remove unused dice and roll the specified quantity of dice
    }
    protected void DrawCards(int quantity)
    {
        // draw count number of cards
    }
    protected void DestroyCardsInHands()
    {
        // destroy all cards in battle parcicipants' hands
    }
    protected void DestroyCardsInDecks()
    {
        // destroy all cards in battle participants' decks
    }
    protected bool TryFuseCard(CardCategories permissibleCardCategories)
    {
        // method signature needs work...
        return false;
    }
    protected int GetDiceCount(DiceFace diceFace, DiceCountType diceCountType)
    {
        // return the requested number of dice
        return 0;
    }
    protected int GetPower(bool myStat = true)
    {
        // return the specified player's power stat
        return 0;
    }
    protected int GetDefense(bool myStat = true)
    {
        // return the specified player's defense stat
        return 0;
    }
    protected int GetWeaponCount(bool myCards = true)
    {
        // return the specified player's number of equipped weapons
        return 0;
    }
    protected int GetArmorCount(bool myCards = true)
    {
        // return the specified player's number of equipped armors
        return 0;
    }    
}

public enum DiceCountType
{
    TotalThisTurn,
    TotalMostRecentDieRoll,
}

public struct CardEffectContext
{
    public BattleManager battleManager;
    public Card parentCard;
}