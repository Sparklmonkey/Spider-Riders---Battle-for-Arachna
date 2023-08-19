using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CardInPlay : MonoBehaviour
{
    [SerializeField] private TextMeshPro _cardName, _attackValue, _defenseValue, _diceValue;
    [SerializeField] private GameObject _attackObject, _defenseObject, _diceObject, _activationFrame;
    [SerializeField] private SpriteRenderer _cardImageRenderer, _cardBackRenderer;
    [SerializeField] private CardInfoButton _cardInfoButton;
    [SerializeField] private float _moveToPositionTime = 0.3f;

    public enum State
    {
        Uninitialized,
        Initialized,
        InPlay,
        Activated,
        Destroying,
    }
    public class References
    {
        public Card CardDefinition { get; private set; }
        public IBattleManager BattleManager { get; private set; }

        public References(CardInPlay thisCard, Card cardDefinition, IBattleManager battleManager)
        {
            CardDefinition = cardDefinition;
            BattleManager = battleManager;
            thisCard._cardName.text = CardDefinition.CardName;
            thisCard._cardBackRenderer.sprite = CardDefinition.CardType.BackgroundSprite;
            thisCard._cardImageRenderer.sprite = CardDefinition.CardImage;
        }
    }
    public class BattleStateData
    {
        public CardInPlay ThisCard { get; private set; }
        private State _state;
        public State State
        {
            get => _state;
            private set
            {
                if (_state == value) return;
                ThisCard._activationFrame.SetActive(value == State.Activated);
                if (_state != State.Activated && value == State.Activated)
                {
                    ApplyOwnerModifiers();
                    ApplyTargetModifiers();
                    ThisCard.Reference.BattleManager.PlayCardActivationSound();
                }
                if (_state == State.Activated && value != State.Activated)
                {
                    RemoveOwnerModifiers();
                    RemoveTargetModifiers();
                }
                _state = value;
            }
        }
        public bool IsUninitialized => State != State.Uninitialized;
        public bool IsInPlay => State == State.InPlay || State == State.Activated || State == State.Destroying;
        public bool IsActivated => State == State.Activated;
        public bool IsDestroying => State == State.Destroying;
        private CardSlot _cardSlot;
        public CardSlot CardSlot
        {
            get => _cardSlot;
            set
            {
                if (_cardSlot == value) return;
                if (!IsUninitialized && CardOwner != null && CardOwner.HandOfCards != null)
                {
                    CardOwner.HandOfCards.TryClearCard(ThisCard);
                }
                _cardSlot = value;
                if (!IsUninitialized && CardOwner != null && CardOwner.HandOfCards != null)
                {
                    CardOwner.HandOfCards.SetCard(ThisCard, value);
                }
            }
        }
        private IBattleParticipant _cardOwner;
        public IBattleParticipant CardOwner
        {
            get => _cardOwner;
            set
            {
                if (_cardOwner == value) return;
                if (!IsUninitialized && _cardOwner != null)
                {
                    if (_cardOwner.HandOfCards != null) _cardOwner.HandOfCards.TryClearCard(ThisCard);
                    if (IsActivated) RemoveOwnerModifiers();
                }
                _cardOwner = value;
                if (!IsUninitialized && value != null)
                {
                    if (value.HandOfCards != null) value.HandOfCards.SetCard(ThisCard, CardSlot);
                    if (IsActivated) ApplyOwnerModifiers();
                }
            }
        }
        private IBattleParticipant _cardTarget;
        public IBattleParticipant CardTarget
        {
            get => _cardTarget;
            set
            {
                if (_cardTarget == value) return;
                if (IsActivated && _cardTarget != null)
                {
                    RemoveTargetModifiers();
                }
                _cardOwner = value;
                if (IsActivated && value != null)
                {
                    ApplyTargetModifiers();
                }
            }
        }
        private int _cardsLeftToDraw;
        public int CardsLeftToDraw
        {
            get => _cardsLeftToDraw;
            set
            {
                if (IsUninitialized)
                {
                    _cardsLeftToDraw = 0;
                    return;
                }
                if ( _cardsLeftToDraw == value) return;
                if (CardOwner != null) CardOwner.AddCardsToDraw(value - _cardsLeftToDraw);
                _cardsLeftToDraw = value;
            }
        }
        private int _elapsedTurnsActive;
        public int ElapsedTurnsActive
        {
            get => _elapsedTurnsActive;
            set
            {
                if (!IsActivated) return;
                _elapsedTurnsActive = value;
                if (_elapsedTurnsActive >= ThisCard.Reference.CardDefinition.TurnsActive && ThisCard.Reference.CardDefinition.TurnsActive >= 0)
                {
                    Expire();
                }
            }
        }
        private int _usedFuses;
        public int UsedFuses
        {
            get => _usedFuses;
            set
            {
                if (IsUninitialized)
                {
                    _usedFuses = 0;
                    return;
                }
                _usedFuses = value;
            }
        }
        public int RemainingFuses => Math.Max((ThisCard.Reference.CardDefinition.CardFuseLimit - UsedFuses), 0);
        private StatModifiers _activeOwnerStatModifiers;
        public StatModifiers ActiveOwnerStatModifiers
        {
            get => _activeOwnerStatModifiers;
            set
            {
                if (IsUninitialized)
                {
                    _activeOwnerStatModifiers = StatModifiers.Empty;
                    return;
                }
                if (_activeOwnerStatModifiers == value) return;
                CardOwner.StatModifiers += (value - _activeOwnerStatModifiers);
                _activeOwnerStatModifiers = value;
                UpdateOwnerStatsVisuals();
            }
        }
        private StatModifiers _activeOpponentStatModifiers;
        public StatModifiers ActiveOpponentStatModifiers
        {
            get => _activeOpponentStatModifiers;
            set
            {
                if (IsUninitialized)
                {
                    _activeOpponentStatModifiers = StatModifiers.Empty;
                    return;
                }
                if (_activeOpponentStatModifiers == value) return;
                CardTarget.StatModifiers += (value - _activeOpponentStatModifiers);
                _activeOpponentStatModifiers = value;
            }
        }

        public BattleStateData(CardInPlay thisCard)
        {
            ThisCard = thisCard;
            State = State.Uninitialized;
        }
        public BattleStateData Initialize(IBattleParticipant cardOwner, CardSlot cardSlot = CardSlot.OnDeck)
        {
            if (State != State.Uninitialized) throw new InvalidOperationException($"CardInPlay must be Uninitialized to initialize: {ThisCard}");
            State = State.Initialized;
            CardSlot = cardSlot;
            CardOwner = cardOwner;
            CardTarget = null;

            _cardsLeftToDraw = 0;
            _elapsedTurnsActive = 0;
            _usedFuses = 0;
            _activeOwnerStatModifiers = StatModifiers.Empty;
            UpdateOwnerStatsVisuals();
            _activeOpponentStatModifiers = StatModifiers.Empty;
            return this;
        }
        public BattleStateData PutInPlay()
        {
            if (State != State.Initialized) throw new InvalidOperationException($"CardInPlay must be Initialized to put in play: {ThisCard}");
            State = State.InPlay;
            return this;
        }
        public BattleStateData Activate()
        {
            if (State != State.InPlay) throw new InvalidOperationException($"CardInPlay must be InPlay to activate: {ThisCard}");
            State = State.Activated;
            return this;
        }
        public BattleStateData Expire()
        {
            if (State != State.Activated) throw new InvalidOperationException($"CardInPlay must be Activated to expire: {ThisCard}");
            ThisCard.StartCoroutine(DestroyInCemetery());
            return this;
        }
        public IEnumerator DestroyInCemetery()
        {
            if (State != State.InPlay && State != State.Activated) throw new InvalidOperationException($"CardInPlay must be in play to destroy: {ThisCard}");
            CardSlot = CardSlot.OnCemetery;
            State = State.Destroying;
            ThisCard._movementCoroutine = ThisCard.StartCoroutine(
                ThisCard.MovementData.MoveToDestination(CardOwner.HandOfCards.GetSlotPosition(CardSlot.OnCemetery), ThisCard._moveToPositionTime, true)
            );
            yield return ThisCard._movementCoroutine;
            CardOwner.HandOfCards.TriggerTrashCanAnimationOrOverlap();
            yield return DestroySelf();
        }
        public IEnumerator DestroyInPlace()
        {
            if (State != State.InPlay && State != State.Activated) throw new InvalidOperationException($"CardInPlay must be in play to destroy: {ThisCard}");
            State = State.Destroying;
            yield return DestroySelf();
        }

        private IEnumerator DestroySelf()
        {
            State = State.Destroying;
            Destroy(ThisCard);
            // Could alternatively have a pool manager set this gameObject to inactive, set State to Uninitialized, and put it back in the pool, etc
            yield break;
        }
        private void ApplyOwnerModifiers()
        {
            if (CardOwner != null)
            {
                CardOwner.AddCardsToDraw(CardsLeftToDraw);
                CardOwner.StatModifiers += ActiveOwnerStatModifiers;
            }
        }
        private void RemoveOwnerModifiers()
        {
            if (CardOwner != null)
            {
                CardOwner.AddCardsToDraw(-CardsLeftToDraw);
                CardOwner.StatModifiers -= ActiveOwnerStatModifiers;
            }
        }
        private void ApplyTargetModifiers()
        {
            if (CardTarget != null)
            {
                CardTarget.StatModifiers += ActiveOpponentStatModifiers;
            }
        }
        private void RemoveTargetModifiers()
        {
            if (CardTarget != null)
            {
                CardTarget.StatModifiers -= ActiveOpponentStatModifiers;
            }
        }
        private void UpdateOwnerStatsVisuals()
        {
            int attackModifier = ActiveOwnerStatModifiers.attackModifier;
            ThisCard._attackValue.text = attackModifier.ToString();
            ThisCard._attackObject.SetActive(attackModifier > 0);

            int defenseModifier = ActiveOwnerStatModifiers.defenseModifier;
            ThisCard._defenseValue.text = defenseModifier.ToString();
            ThisCard._defenseObject.SetActive(defenseModifier > 0);

            int diceModifier = ActiveOwnerStatModifiers.diceModifier;
            ThisCard._diceValue.text = diceModifier.ToString();
            ThisCard._diceObject.SetActive(diceModifier > 0);
        }
    }

    public References Reference { get; private set; }
    public BattleStateData BattleState { get; private set; }
    public MovementDragSnap MovementData { get; private set; }
    
    private Camera _mainCamera;
    private Coroutine _movementCoroutine;

    private void OnEnable()
    {
        if (BattleState.IsUninitialized) throw new InvalidOperationException($"CardInPlay must be Initialized to enable: {this}");
        _mainCamera = Camera.main;
        if (BattleState.IsInPlay) return;
        BattleState.PutInPlay();
    }
    private void OnMouseDown()
    {
        if (_movementCoroutine != null) return;
        MovementData.DragPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _movementCoroutine = StartCoroutine(MovementData.Drag());
    }
    private void OnMouseDrag()
    {
        if (MovementData.IsDragging) MovementData.DragPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
    private void OnMouseUp()
    {
        if (!MovementData.IsDragging) return;
        Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        IHandOfCardsManager hand = BattleState.CardOwner.HandOfCards;

        if (hand.TryGetSlot(mousePosition, out CardSlot slot))
        {
            if (slot == CardSlot.OnCemetery)
            {
                BattleState.DestroyInCemetery();
            }
            else
            {
                if (hand.TryGetCardInSlot(slot, out CardInPlay cardInSlot))
                {
                    StartCoroutine(cardInSlot.DropCardOnto(this));
                }
                else
                {
                    if (slot != CardSlot.OnDeck) BattleState.CardSlot = slot;
                    _movementCoroutine = StartCoroutine(MovementData.MoveToDestination(hand.GetSlotPosition(slot), _moveToPositionTime, true));
                }
            }
        }
        else
        {
            _movementCoroutine = StartCoroutine(MovementData.MoveToDestination(hand.GetSlotPosition(BattleState.CardSlot), _moveToPositionTime, true));
        }
    }

    public CardInPlay Initialize(Card cardDefinition,
        IBattleManager battleManager,
        IBattleParticipant cardOwner,
        CardSlot cardSlot)
    {
        Reference = new References(this, cardDefinition, battleManager);
        MovementData = new MovementDragSnap(transform).TeleportToPosition(cardOwner.HandOfCards.GetSlotPosition(cardSlot));
        // _mainCamera is initialized in OnEnable.
        _movementCoroutine = null;
        BattleState = new BattleStateData(this).Initialize(cardOwner, cardSlot);
        return this;
    }
    public IEnumerator InvokeActivateEffect()
    {
        CardEffect activateEffect = Reference.CardDefinition.OnActivatedCardEffect;
        if (activateEffect == null) yield break;
        yield return StartCoroutine(activateEffect.Invoke(new CardEffectContext
        {
            thisCardInstance = this,
        }));
    }
    public IEnumerator InvokeDeactivateEffect()
    {
        CardEffect deactivateEffect = Reference.CardDefinition.OnDeactivatedCardEffect;
        if (deactivateEffect == null) yield break;
        yield return StartCoroutine(deactivateEffect.Invoke(new CardEffectContext
        {
            thisCardInstance = this,
        }));
    }
    public IEnumerator DropCardOnto(CardInPlay droppedCard)
    {
        // Continue until there is one card remaining (FusingComplete)
        if (IsFusingComplete(droppedCard)) throw new InvalidOperationException($"DropCardOnto was somehow called between {this} and {droppedCard}, but one or more is not initialized...");
        // 0. Move the cards to the same position
        droppedCard._movementCoroutine = StartCoroutine(
            droppedCard.MovementData.MoveToDestination(droppedCard.BattleState.CardOwner.HandOfCards.GetSlotPosition(BattleState.CardSlot), droppedCard._moveToPositionTime, true)
        );
        // 1. Use this card's drop effect - drop effects should check for card activation status
        if (Reference.CardDefinition.OnDroppedOntoCardEffect != null)
        {
            yield return StartCoroutine(Reference.CardDefinition.OnDroppedOntoCardEffect.Invoke(new CardEffectContext
            {
                thisCardInstance = this,
                otherCardInstance = droppedCard,
            }));
            if (IsFusingComplete(droppedCard)) yield break;
        }
        // 2. Use the other card's drop effect - drop effects should check for card activation status
        if (droppedCard.Reference.CardDefinition.OnDroppedOntoCardEffect != null)
        {
            yield return StartCoroutine(droppedCard.Reference.CardDefinition.OnDroppedOntoCardEffect.Invoke(new CardEffectContext
            {
                thisCardInstance = droppedCard,
                otherCardInstance = this,
            }));
            if (droppedCard == null) yield break;
        }
        // 3. Swap the cards' positions
        _movementCoroutine = StartCoroutine(MovementData.MoveToDestination(BattleState.CardOwner.HandOfCards.GetSlotPosition(droppedCard.BattleState.CardSlot), _moveToPositionTime, true));
        CardSlot temporarySlot = BattleState.CardSlot;
        BattleState.CardSlot = droppedCard.BattleState.CardSlot;
        droppedCard.BattleState.CardSlot = temporarySlot;
    }
    private bool IsFusingComplete(CardInPlay secondCard)
    {
        return (
            secondCard == null
            || secondCard.BattleState == null
            || secondCard.BattleState.IsUninitialized
            || secondCard.BattleState.IsDestroying
            || this == null
            || this.BattleState == null
            || this.BattleState.IsUninitialized
            || this.BattleState.IsDestroying
        );
    }
}
