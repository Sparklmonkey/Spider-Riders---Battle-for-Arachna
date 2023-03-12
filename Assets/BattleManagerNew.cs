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

    private MobData _mobData;
    private Animator _mobAnimator, _playerAnimator;
    [SerializeField]
    private Transform _enemyContainer, _playercontainer;
    [SerializeField]
    private TextMeshProUGUI _playerAtkLbl, _playerDefLbl, _playerHealthLbl, _enemyAtkLbl, _enemyDefLbl, _enemyHealthLbl;

    public bool IsPlayerTurn;
    public void SetupBattleManager(MobData mobData)
    {
        _mobData = mobData;
        _enemyAtkLbl.text = mobData.stats.power.ToString();
        _enemyDefLbl.text = mobData.stats.defense.ToString();
        _enemyHealthLbl.text = mobData.stats.health.ToString();

        _playerAtkLbl.text = TestPlayer<PlayerData>.GetPower().ToString();
        _playerDefLbl.text = TestPlayer<PlayerData>.GetDefense().ToString();
        _playerHealthLbl.text = TestPlayer<PlayerData>.GetHealth().ToString();

        _mobAnimator = Instantiate(_mobData.enemyPrefab, _enemyContainer).GetComponent<Animator>();
        _mobAnimator.Play("Idle");

        IsPlayerTurn = true;//Random.Range(0, 2) == 1;
    }

    private Stats _playerModifiers, _enemyModifiers;

    public void RollDice()
    {
        if (IsPlayerTurn)
        {
            DicePoolManager.Instance.RollDiceForTurn(TestPlayer<PlayerData>.GetPower() + _playerModifiers.power, _playerAtkLbl.transform);
        }
    }

    public void AddRedDie()
    {
        if (IsPlayerTurn)
        {
            _playerAtkLbl.text = (int.Parse(_playerAtkLbl.text) + 1).ToString();
        }
    }


    [ContextMenu("Test Setup")]
    public void TestSetup()
    {
        SetupBattleManager(TestMob);
    }
}
