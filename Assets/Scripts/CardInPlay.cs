using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInPlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cardName, _attackValue, _defenseValue, _diceValue;
    [SerializeField] private GameObject _attackObject, _defenseObject, _diceObject;
    [SerializeField] private Image _cardImage, _cardBack;
    [SerializeField] private CardInfoButton _cardInfoButton;

    public Card CardDefinition { get; private set; }
    public IBattleManager BattleManager { get; private set; }
    public IBattleParticipant CardOwner { get; set; }
    public IBattleParticipant CardTarget { get; set; }
    public CardSlot CardSlot { get; set; }
    public bool IsActivated { get; set; }
    public int CardsToDraw { get; set; }
    public int ElapsedTurnsActive { get; set; }
    public int UsedFuses { get; set; }
    public List<StatModifier> ActiveOwnerStatModifiers { get; set; }
    public List<StatModifier> ActiveOpponentStatModifiers { get; set; }

    private bool _isInitialized = false;

    private bool _isDragging = false;
    private Vector3 _targetPosition;
    private Vector3 _snapStartPosition;
    [SerializeField] private float _snapToPositionTime = 0.3f;
    private float _snapToPositionElapsedTime = 0f;

    private void OnEnable()
    {
        if (!_isInitialized) Debug.LogAssertion($"Please initialize CardInPlay prior to enabling!");
        _cardName.text = CardDefinition.CardName;
        _attackValue.text = string.Empty;
        _defenseValue.text = string.Empty;
        _diceValue.text = string.Empty;
        _attackObject.SetActive(false);
        _defenseObject.SetActive(false);
        _diceObject.SetActive(false);
        foreach (StatModifier modifier in CardDefinition.ActivatedOwnerStatModifiers)
        {
            switch(modifier.statType)
            {
                case StatType.Attack:
                    _attackValue.text = modifier.amount.ToString();
                    _attackObject.SetActive(true);
                    break;
                case StatType.Defense:
                    _defenseValue.text = modifier.amount.ToString();
                    _defenseObject.SetActive(true);
                    break;
                case StatType.Dice:
                    _diceValue.text = modifier.amount.ToString();
                    _diceObject.SetActive(true);
                    break;
            }
        }
        _cardBack.sprite = CardDefinition.CardType.BackgroundSprite;
        _cardImage.sprite = CardDefinition.CardImage;
    }
    private void OnDisable()
    {
        if (_isInitialized && IsActivated)
        {
            Deactivate();
        }
        _isInitialized = false;
    }
    private void Update()
    {
        if (_isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _targetPosition = new Vector3(mousePosition.x, mousePosition.y, 0);
            _snapToPositionElapsedTime = 0f;
            _snapStartPosition = _targetPosition;
        }
        else
        {
            _targetPosition = CardSlotPosition(CardSlot);
        }
        if (transform.position != _targetPosition)
        {
            transform.position = Vector3.Lerp(_snapStartPosition, _targetPosition, _snapToPositionElapsedTime / _snapToPositionTime);
        }
    }
    private void OnMouseDown()
    {
        _isDragging = true;
    }
    private void OnMouseUp()
    {
        _isDragging = false;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (TryGetCardInTouchedSlot(mousePosition, out CardInPlay cardDroppedOnto))
        {
            cardDroppedOnto.DropCardOnto(this);
        }
        else if (TryGetTouchedSlotPosition(mousePosition, out Vector3 slotPosition))
        {
            _targetPosition = slotPosition;
        }
        else
        {
            _targetPosition = CardSlotPosition(CardSlot);
        }
    }

    public CardInPlay Initialize(Card cardDefinition,
        IBattleManager battleManager,
        IBattleParticipant cardOwner,
        CardSlot cardSlot)
    {
        CardDefinition = cardDefinition;
        BattleManager = battleManager;
        CardOwner = cardOwner;
        CardTarget = null;
        CardSlot = cardSlot;
        IsActivated = false;
        CardsToDraw = cardDefinition.CardsToDraw;
        ElapsedTurnsActive = 0;
        UsedFuses = 0;
        ActiveOwnerStatModifiers = new List<StatModifier>();
        ActiveOpponentStatModifiers = new List<StatModifier>();

        _targetPosition = _snapStartPosition = CardSlotPosition(cardSlot);
        _isInitialized = true;
        return this;
    }
    public void Activate()
    {
        CardDefinition.OnActivatedCardEffect?.Invoke(new CardEffectContext
        {
            thisCardInstance = this,
        });
    }
    public void Deactivate()
    {
        CardDefinition.OnDeactivatedCardEffect?.Invoke(new CardEffectContext
        {
            thisCardInstance = this,
        });
    }
    public void DropCardOnto(CardInPlay droppedCard)
    {
        CardDefinition.OnDroppedOntoCardEffect?.Invoke(new CardEffectContext
        {
            thisCardInstance = this,
            otherCardInstance = droppedCard,
        });
    }

    public static bool TryGetCardInTouchedSlot(Vector3 referencePosition, out CardInPlay cardInTouchedSlot)
    {
        throw new NotImplementedException();
    }
    public static bool TryGetTouchedSlotPosition(Vector3 referencePosition, out Vector3 slotPosition)
    {
        throw new NotImplementedException();
    }
    public static Vector3 CardSlotPosition(CardSlot cardSlot)
    {
        throw new NotImplementedException();
    }
}
