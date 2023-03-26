using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleManagerNew : MonoBehaviour
{
    public MobData TestMob;
    private static BattleManagerNew _instance;
    public static BattleManagerNew Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private MobData _mobData;
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
    public void SetupBattleManager(MobData mobData)
    {
        _playerModifiers = _enemyModifiers = new Stats();
        _mobData = mobData;
        _enemyAtkLbl.DisplayNumber(mobData.stats.power);
        _enemyDefLbl.DisplayNumber(mobData.stats.defense);
        _enemyHealthLbl.DisplayNumber(mobData.stats.health);

        _playerAtkLbl.DisplayNumber(TestPlayer<PlayerData>.GetPower());
        _playerDefLbl.DisplayNumber(TestPlayer<PlayerData>.GetDefense());
        _playerHealthLbl.DisplayNumber(TestPlayer<PlayerData>.GetHealth());
        GameObject mobObject = Instantiate(_mobData.enemyPrefab, _enemyContainer);
        _mobAnimator = mobObject.AddComponent<MobAnimationManager>();
        _mobAnimator.SetupManager(mobData.mobName);
        _playerAnimator.PlayAnimation("Idle");

        IsPlayerTurn = true;
    }

    private Stats _playerModifiers, _enemyModifiers;

    public void RollDice()
    {
        StartCoroutine(DicePoolManager.Instance.RollDiceForTurn(TestPlayer<PlayerData>.GetPower() + _playerModifiers.power, _playerAtkLocation));
    }

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
        DicePoolManager.Instance.ClearDice();
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
        if (!IsPlayerTurn)
        {
            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator EnemyTurn()
    {
        if(_mobData.stats.power == 0)
        {
            EndTurn();
            yield return new WaitForSeconds(2f);
            yield break;
        }
        yield return StartCoroutine(DicePoolManager.Instance.RollDiceForTurn(TestPlayer<PlayerData>.GetPower() + _enemyModifiers.power, _enemyAtkLocation));
    }

    [ContextMenu("Test Setup")]
    public void TestSetup()
    {
        SetupBattleManager(TestMob);
    }
}
