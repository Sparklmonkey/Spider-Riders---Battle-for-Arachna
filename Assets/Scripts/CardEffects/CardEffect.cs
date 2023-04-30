using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class CardEffect : ScriptableObject
{
    public abstract void Invoke(CardEffectContext cardEffectContext);

    protected bool TryActivate(CardInPlay cardInstance)
    {
        if (cardInstance.IsActivated) return false;
        cardInstance.IsActivated = true;
        return true;
    }
    protected void HealToFull(CardInPlay thisCardInstance, bool applyToOpponent = false)
    {
        // return if health is already >= max
        // heal up to full health
    }
    protected void Heal(CardInPlay thisCardInstance, int healAmount, bool applyToOpponent = false)
    {
        // heal by the specified amount
    }
    protected void HealCapped(CardInPlay thisCardInstance, int healAmount, bool applyToOpponent = false)
    {
        // heal by the specified amount, but don't go over max health
    }
    protected void DestroyCard(CardInPlay thisCardInstance, CardTypeNew cardType, bool applyToOpponent = true)
    {
        // return if there is no card of the specified type in the hand
        // if AI player, automatically select a card to destroy and destroy it
        // if human player, pop up destruction UI keyed to the cardType
    }
    protected void DestroyCard(CardInPlay thisCardInstance, CardCategories permissibleCardCategories, bool applyToOpponent = true)
    {
        // return if there is no card of the specified type in the hand
        // if AI player, automatically select a card to destroy and destroy it
        // if human player, pop up destruction UI keyed to the permissibleCardCategories
    }
    protected void ApplyPredefinedStatModifiers(CardInPlay thisCardInstance)
    {
        thisCardInstance.IsActivated = true;
        foreach (StatModifier modifier in thisCardInstance.CardDefinition.ActivatedOwnerStatModifiers)
        {
            ApplyStatModifier(thisCardInstance, modifier, false);
        }
        foreach (StatModifier modifier in thisCardInstance.CardDefinition.ActivatedOpponentStatModifiers)
        {
            ApplyStatModifier(thisCardInstance, modifier, true);
        }
    }
    protected void ApplyStatModifier(CardInPlay thisCardInstance, StatModifier statModifier, bool applyToOpponent = false)
    {
        // apply the specified stat modifier
    }
    protected void RollNewDice(CardInPlay thisCardInstance)
    {
        // remove unused dice and roll new dice
    }
    protected void RollSomeDice(CardInPlay thisCardInstance, int quantity)
    {
        // remove unused dice and roll the specified quantity of dice
    }
    protected void DrawCards(CardInPlay thisCardInstance, int quantity)
    {
        // draw count number of cards
    }
    protected void DestroyCardsInHands(CardInPlay thisCardInstance)
    {
        // destroy all cards in battle parcicipants' hands
    }
    protected void DestroyCardsInDecks(CardInPlay thisCardInstance)
    {
        // destroy all cards in battle participants' decks
    }
    protected bool TryFuseCard(CardInPlay thisCardInstance, CardInPlay otherCardInstance, CardCategories permissibleCardCategories)
    {
        // check if the other card is OK to fuse; fuse if OK, else bounce it back to its original slot
        return false;
    }
    protected int GetDiceCount(CardInPlay thisCardInstance, DiceFace diceFace, DiceCountType diceCountType)
    {
        // return the requested number of dice
        return 0;
    }
    protected int GetBaseAttack(CardInPlay thisCardInstance, bool myStat = true)
    {
        // return the specified player's attack stat
        return 0;
    }
    protected int GetCurrentAttack(CardInPlay thisCardInstance, bool myStat = true)
    {
        // return the specified player's attack stat
        return 0;
    }
    protected int GetBaseDefense(CardInPlay thisCardInstance, bool myStat = true)
    {
        // return the specified player's defense stat
        return 0;
    }
    protected int GetCurrentDefense(CardInPlay thisCardInstance, bool myStat = true)
    {
        // return the specified player's defense stat
        return 0;
    }
    protected int GetWeaponCount(CardInPlay thisCardInstance, bool myCards = true)
    {
        // return the specified player's number of equipped weapons
        return 0;
    }
    protected int GetArmorCount(CardInPlay thisCardInstance, bool myCards = true)
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

public interface IBattleManager
{
}

public struct CardEffectContext
{
    public CardInPlay thisCardInstance;
    public CardInPlay otherCardInstance;
}