using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardInPlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cardName, _attackValue, _defenseValue, _diceValue;
    [SerializeField] private GameObject _attackObject, _defenseObject, _diceObject, _activationFrame;
    [SerializeField] private Image _cardImage, _cardBack;
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
            thisCard._cardBack.sprite = CardDefinition.CardType.BackgroundSprite;
            thisCard._cardImage.sprite = CardDefinition.CardImage;
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
                _state = value;
                if (_state == State.Destroying) PrepareForDestruction();
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
                Debug.LogWarning($"CardInPlay {ThisCard} switching slot from {_cardSlot} - clean up connection");
                _cardSlot = value;
                Debug.LogWarning($"CardInPlay {ThisCard} switching slot to {value} - set up connection");
            }
        }
        private IBattleParticipant _cardOwner;
        public IBattleParticipant CardOwner
        {
            get => _cardOwner;
            set
            {
                if (_cardOwner == value) return;
                Debug.LogWarning($"CardInPlay {ThisCard} switching owner from {_cardOwner} - clean up connection");
                _cardOwner = value;
                Debug.LogWarning($"CardInPlay {ThisCard} switching owner to {value} - set up connection");
            }
        }
        private IBattleParticipant _cardTarget;
        public IBattleParticipant CardTarget
        {
            get => _cardTarget;
            set
            {
                if (_cardTarget == value) return;
                Debug.LogWarning($"CardInPlay {ThisCard} switching target from {_cardTarget} - clean up connection");
                _cardOwner = value;
                Debug.LogWarning($"CardInPlay {ThisCard} switching target to {value} - set up connection");
            }
        }
        private int _cardsLeftToDraw;
        public int CardsLeftToDraw
        {
            get => _cardsLeftToDraw;
            set
            {
                if ( _cardsLeftToDraw == value) return;
                Debug.LogWarning($"CardInPlay {ThisCard} used to allow {_cardsLeftToDraw} more draws, now {value} - notify appropriate parties");
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
                if (!IsActivated) return;
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
                if (!IsActivated) return;
                if (_activeOwnerStatModifiers == value) return;
                Debug.LogWarning($"CardInPlay {ThisCard} owner modifiers changing from {_activeOwnerStatModifiers} to {value} - notify appropriate parties");
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
                if (!IsActivated) return;
                if (_activeOpponentStatModifiers == value) return;
                Debug.LogWarning($"CardInPlay {ThisCard} opponent modifiers changing from {_activeOpponentStatModifiers} to {value} - notify appropriate parties");
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
            CardsLeftToDraw = 0;
            _elapsedTurnsActive = 0;
            _usedFuses = 0;
            _activeOwnerStatModifiers = new StatModifiers();
            UpdateOwnerStatsVisuals();
            _activeOpponentStatModifiers = new StatModifiers();
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
            Debug.LogWarning($"CardInPlay {ThisCard} activated - play a sound, etc!");
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
                ThisCard.MovementData.MoveToDestination(CardOwner.GetSlotPosition(CardSlot.OnCemetery), ThisCard._moveToPositionTime, true)
            );
            yield return ThisCard._movementCoroutine;
            yield return DestroySelf();
        }
        public IEnumerator DestroyInPlace()
        {
            if (State != State.InPlay && State != State.Activated) throw new InvalidOperationException($"CardInPlay must be in play to destroy: {ThisCard}");
            State = State.Destroying;
            yield return DestroySelf();
        }

        private void PrepareForDestruction()
        {
            Debug.LogWarning($"CardInPlay {ThisCard} preparing for destruction - clean up stats, connections, etc!");
        }
        private IEnumerator DestroySelf()
        {
            Debug.LogWarning($"CardInPlay {ThisCard} is selfdestructing - implement this...");
            yield break;
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
    private void Update()
    {
        if (MovementData.IsDragging) MovementData.DragPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
    private void OnMouseDown()
    {
        if (_movementCoroutine != null) return;
        MovementData.DragPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _movementCoroutine = StartCoroutine(MovementData.Drag());
    }
    private void OnMouseUp()
    {
        if (!MovementData.IsDragging) return;
        Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        IBattleParticipant cardOwner = BattleState.CardOwner;

        if (cardOwner.TryGetSlot(mousePosition, out CardSlot slot))
        {
            switch (slot)
            {
                case CardSlot.OnCemetery:
                    BattleState.DestroyInCemetery();
                    break;
                case CardSlot.OnDeck:
                    _movementCoroutine = StartCoroutine(MovementData.MoveToDestination(cardOwner.GetSlotPosition(BattleState.CardSlot), _moveToPositionTime, true));
                    break;
                default:
                    if (cardOwner.TryGetCardInSlot(slot, out CardInPlay cardInSlot))
                    {
                        cardInSlot.DropCardOnto(this);
                    }
                    else
                    {
                        BattleState.CardSlot = slot;
                        _movementCoroutine = StartCoroutine(MovementData.MoveToDestination(cardOwner.GetSlotPosition(slot), _moveToPositionTime, true));
                    }
                    break;
            }
        }
        else
        {
            _movementCoroutine = StartCoroutine(MovementData.MoveToDestination(cardOwner.GetSlotPosition(BattleState.CardSlot), _moveToPositionTime, true));
        }
    }

    public CardInPlay Initialize(Card cardDefinition,
        IBattleManager battleManager,
        IBattleParticipant cardOwner,
        CardSlot cardSlot)
    {
        Reference = new References(this, cardDefinition, battleManager);
        MovementData = new MovementDragSnap(transform).TeleportToPosition(cardOwner.GetSlotPosition(cardSlot));
        // _mainCamera is initialized in OnEnable.
        _movementCoroutine = null;
        BattleState = new BattleStateData(this).Initialize(cardOwner, cardSlot);
        return this;
    }
    public void InvokeActivateEffect()
    {
        CardEffect activateEffect = Reference.CardDefinition.OnActivatedCardEffect;
        if (activateEffect == null) return;
        activateEffect.Invoke(new CardEffectContext
        {
            thisCardInstance = this,
        });
    }
    public void InvokeDeactivateEffect()
    {
        CardEffect deactivateEffect = Reference.CardDefinition.OnDeactivatedCardEffect;
        if (deactivateEffect == null) return;
        deactivateEffect.Invoke(new CardEffectContext
        {
            thisCardInstance = this,
        });
    }
    public void DropCardOnto(CardInPlay droppedCard)
    {
        // Continue until there is one card remaining (FusingComplete)
        if (IsFusingComplete(droppedCard)) throw new InvalidOperationException($"DropCardOnto was somehow called between {this} and {droppedCard}, but one or more is not initialized...");
        // 0. Move the cards to the same position
        droppedCard._movementCoroutine = StartCoroutine(
            droppedCard.MovementData.MoveToDestination(droppedCard.BattleState.CardOwner.GetSlotPosition(BattleState.CardSlot), droppedCard._moveToPositionTime, true)
        );
        // 1. Use this card's drop effect - drop effects should check for card activation status
        if (Reference.CardDefinition.OnDroppedOntoCardEffect != null)
        {
            Reference.CardDefinition.OnDroppedOntoCardEffect.Invoke(new CardEffectContext
            {
                thisCardInstance = this,
                otherCardInstance = droppedCard,
            });
            if (IsFusingComplete(droppedCard)) return;
        }
        // 2. Use the other card's drop effect - drop effects should check for card activation status
        if (droppedCard.Reference.CardDefinition.OnDroppedOntoCardEffect != null)
        {
            droppedCard.Reference.CardDefinition.OnDroppedOntoCardEffect.Invoke(new CardEffectContext
            {
                thisCardInstance = droppedCard,
                otherCardInstance = this,
            });
            if (droppedCard == null) return;
        }
        // 3. Swap the cards' positions
        _movementCoroutine = StartCoroutine(MovementData.MoveToDestination(BattleState.CardOwner.GetSlotPosition(droppedCard.BattleState.CardSlot), _moveToPositionTime, true));
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
