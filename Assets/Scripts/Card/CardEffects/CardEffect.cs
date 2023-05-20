using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class CardEffect : ScriptableObject
{
    public abstract void Invoke(CardEffectContext cardEffectContext);

    protected bool TryActivate(CardInPlay cardInstance)
    {
        if (cardInstance.BattleState.State == CardInPlay.State.InPlay) return false;
        cardInstance.BattleState.Activate();
        return true;
    }
    protected IBattleParticipant RequestTarget(CardInPlay thisCardInstance)
    {
        // thisCardInstance.BattleManager.MethodName(thisCardInstance)
        // Ask for a target, set the CardInPlay's target
        return null;
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
    protected void TargetAndDestroyACard(CardInPlay thisCardInstance, CardType cardType, bool applyToOpponent = true)
    {
        // return if there is no card of the specified type in the hand
        // if AI player, automatically select a card to destroy and destroy it
        // if human player, pop up destruction UI keyed to the cardType
    }
    protected void TargetAndDestroyACard(CardInPlay thisCardInstance, CardCategories permissibleCardCategories, bool applyToOpponent = true)
    {
        // return if there is no card of the specified type in the hand
        // if AI player, automatically select a card to destroy and destroy it
        // if human player, pop up destruction UI keyed to the permissibleCardCategories
    }
    protected void SetPredefinedStatModifiers(CardInPlay thisCardInstance)
    {
        OverwriteStatModifiers(thisCardInstance, thisCardInstance.Reference.CardDefinition.ActivatedOwnerStatModifiers, false);
        OverwriteStatModifiers(thisCardInstance, thisCardInstance.Reference.CardDefinition.ActivatedOpponentStatModifiers, true);
    }
    protected void OverwriteStatModifiers(CardInPlay thisCardInstance, StatModifiers newStatModifiers, bool applyToOpponent = false)
    {
        if (applyToOpponent)
        {
            thisCardInstance.BattleState.ActiveOpponentStatModifiers = newStatModifiers;
        }
        else
        {
            thisCardInstance.BattleState.ActiveOwnerStatModifiers = newStatModifiers;
        }
    }
    protected void ApplyStatModifiers(CardInPlay thisCardInstance, StatModifiers statModifiers, bool applyToOpponent = false)
    {
        if (applyToOpponent)
        {
            thisCardInstance.BattleState.ActiveOpponentStatModifiers += statModifiers;
        }
        else
        {
            thisCardInstance.BattleState.ActiveOwnerStatModifiers += statModifiers;
        }
    }
    protected void ClearStatModifiers(CardInPlay thisCardInstance, bool applyToOpponent = false)
    {
        if (applyToOpponent)
        {
            thisCardInstance.BattleState.ActiveOpponentStatModifiers = StatModifiers.Empty;
        }
        else
        {
            thisCardInstance.BattleState.ActiveOwnerStatModifiers = StatModifiers.Empty;
        }
    }
    protected void RollNewDice(CardInPlay thisCardInstance)
    {
        // thisCardInstance.Reference.BattleManager.ClearDice();
        // StartCoroutine(thisCardInstance.BattleManager.RollDice());
    }
    protected void RollSomeDice(CardInPlay thisCardInstance, int quantity)
    {
        // thisCardInstance.Reference.BattleManager.ClearDice();
        // StartCoroutine(thisCardInstance.BattleManager.RollDice(quantity));
    }
    protected void DrawCards(CardInPlay thisCardInstance, int quantity)
    {
        thisCardInstance.BattleState.CardsLeftToDraw += quantity;
    }
    protected void DestroyCardsInHands(CardInPlay thisCardInstance)
    {
        // destroy all cards in battle parcicipants' hands
    }
    protected void DestroyCardsInDecks(CardInPlay thisCardInstance)
    {
        // destroy all cards in battle participants' decks
    }
    protected bool TryFuseCard(CardInPlay thisCardInstance, CardInPlay otherCardInstance, CardType cardType)
    {
        if (thisCardInstance.BattleState.RemainingFuses <= 0) return false;
        if (otherCardInstance.Reference.CardDefinition.CardType == cardType)
        {
            FuseCard(thisCardInstance, otherCardInstance);
            return true;
        }
        return false;
    }
    protected bool TryFuseCard(CardInPlay thisCardInstance, CardInPlay otherCardInstance, CardCategories permissibleCardCategories)
    {
        if (thisCardInstance.BattleState.RemainingFuses <= 0) return false;
        CardCategories otherCardCategories = otherCardInstance.Reference.CardDefinition.CardCategories;
        if ((otherCardCategories.isWeapon && permissibleCardCategories.isWeapon)
            || (otherCardCategories.isArmor && permissibleCardCategories.isArmor)
            || (otherCardCategories.isItem && permissibleCardCategories.isItem))
        {
            FuseCard(thisCardInstance, otherCardInstance);
            return true;
        }
        return false;
    }
    private void FuseCard(CardInPlay cardToKeep, CardInPlay cardToFuseIn)
    {
        // Absorb cardToFuseIn's modifiers and other active effects
        cardToKeep.BattleState.ActiveOwnerStatModifiers += cardToFuseIn.BattleState.ActiveOwnerStatModifiers;
        cardToKeep.BattleState.ActiveOpponentStatModifiers += cardToFuseIn.BattleState.ActiveOpponentStatModifiers;
        cardToKeep.BattleState.CardsLeftToDraw += cardToFuseIn.BattleState.CardsLeftToDraw;
        cardToKeep.BattleState.UsedFuses -= cardToFuseIn.BattleState.RemainingFuses;
        // Delete cardToFuseIn
        cardToFuseIn.InvokeDeactivateEffect();
        Debug.LogWarning($"Destroying {cardToFuseIn} (possibly with animations) (functionality not yet implemented)");

        cardToKeep.BattleState.UsedFuses++;
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

public struct CardEffectContext
{
    public CardInPlay thisCardInstance;
    public CardInPlay otherCardInstance;
}