using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public MobData TestMob;
    private void Update()
    {
        if (_isVictory)
        {
            _isVictory = false;
            Destroy(_mobAnimator.gameObject);
            MapManager.Instance.BattleVictory();
        }
    }

    public event EventHandler<BattleManagerEventArgs> OnChangeTurn;
    public class BattleManagerEventArgs : EventArgs
    {
        public bool IsPlayerTurn;
    }

    private bool _isVictory;
    private MobDataStruct _mobData;
    [SerializeField]
    private DicePoolManager _dicePoolManager;
    [SerializeField]
    private PlayerBattleManager _playerAnimator;
    private MobAnimationManager _mobAnimator;
    [SerializeField]
    private DamageDisplayManager _damageDisplayManager;
    [SerializeField]
    private GameObject _damageValuePrefab;
    [SerializeField]
    private Transform _enemyContainer, _playercontainer, _enemyAtkLocation, _playerAtkLocation;
    [SerializeField]
    private NumberDisplayManager _playerAtkLbl, _playerDefLbl, _playerHealthLbl, _enemyAtkLbl, _enemyDefLbl, _enemyHealthLbl;

    public bool IsPlayerTurn = true;
    public void SetupBattleManager(MobDataStruct mobData)
    {
        _playerModifiers = _enemyModifiers = new BattleParticipantStats();
        _mobData = mobData;
        _enemyAtkLbl.DisplayNumber(mobData.stats.attack);
        _enemyDefLbl.DisplayNumber(mobData.stats.defense);
        _enemyHealthLbl.DisplayNumber(mobData.stats.health);

        _playerAtkLbl.DisplayNumber(TestPlayer<PlayerData>.GetAttack());
        _playerDefLbl.DisplayNumber(TestPlayer<PlayerData>.GetDefense());
        _playerHealthLbl.DisplayNumber(TestPlayer<PlayerData>.GetHealth());
        GameObject mobObject = Instantiate(Resources.Load<GameObject>($"Enemies/Mob"), _enemyContainer);
        _mobAnimator = mobObject.AddComponent<MobAnimationManager>();
        _mobAnimator.SetupManager(mobData.mobName);
        _playerAnimator.PlayAnimation("Idle");

        IsPlayerTurn = true;
        OnChangeTurn?.Invoke(this, new BattleManagerEventArgs { IsPlayerTurn = IsPlayerTurn });
    }

    private BattleParticipantStats _playerModifiers, _enemyModifiers;

    public IEnumerator RollDice()
    {
        AddRedDiceAction = AddRedDie;
        yield return StartCoroutine(_dicePoolManager.RollDiceForTurn(TestPlayer<PlayerData>.GetAttack() + _playerModifiers.attack, _playerAtkLocation));
        yield return StartCoroutine(_dicePoolManager.MoveRedDice(AddRedDiceAction));
    }
    public Action AddRedDiceAction;
    public void AddRedDie()
    {
        if (IsPlayerTurn)
        {
            _playerAtkLbl.DisplayNumber(_playerAtkLbl.NumberOnDisplay + 1);
        }
        else
        {
            _enemyAtkLbl.DisplayNumber(_enemyAtkLbl.NumberOnDisplay + 1);
        }
    }

    public IEnumerator ShowDamageTakenAnim(int damage)
    {
        var numberDisplayer = Instantiate(_damageValuePrefab, (IsPlayerTurn ? _enemyContainer : _playercontainer)).GetComponent<NumberDisplayManager>();
        StartCoroutine(numberDisplayer.MoveDamageText(damage));
        if (damage > _mobData.stats.health)
        {
            StartCoroutine(_mobAnimator.PlayDeathAnim());
            yield return StartCoroutine(IsPlayerTurn ? _playerAnimator.PlayPlayerAttackAnim() : _mobAnimator.PlayMobAttackAnim());
            yield return StartCoroutine(_playerAnimator.PlayVictoryAnim()); 
            _isVictory = true;
        }
        else
        {
            StartCoroutine(IsPlayerTurn ? _playerAnimator.PlayPlayerAttackAnim() : _mobAnimator.PlayMobAttackAnim());
            yield return StartCoroutine(_mobAnimator.PlayTakeDamageAnim());
            _playerAnimator.PlayIdleAnimation();
        }
        
    }

    public IEnumerator AttackOpponent()
    {
        _dicePoolManager.ClearDice();
        int result = _playerAtkLbl.NumberOnDisplay - _enemyDefLbl.NumberOnDisplay;
        result = result > 0 ? result : 0;
        yield return _damageDisplayManager.StartDamageCalculations(_playerAtkLbl.transform.parent.gameObject,
            _enemyDefLbl.transform.parent.gameObject,
            _playerHealthLbl.transform.parent.gameObject, result);
        yield return StartCoroutine(ShowDamageTakenAnim(result));
    }

    public void PlayAttackAnimation()
    {
    }
    private void EndTurn()
    {
        IsPlayerTurn = !IsPlayerTurn;
        OnChangeTurn?.Invoke(this, new BattleManagerEventArgs { IsPlayerTurn = IsPlayerTurn });
        if (!IsPlayerTurn)
        {
            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator EnemyTurn()
    {
        if(_mobData.stats.attack == 0)
        {
            EndTurn();
            yield return new WaitForSeconds(2f);
            yield break;
        }
        yield return StartCoroutine(_dicePoolManager.RollDiceForTurn(TestPlayer<PlayerData>.GetAttack() + _enemyModifiers.attack, _enemyAtkLocation));
    }

    [ContextMenu("Test Setup")]
    public void TestSetup()
    {

    }
}
