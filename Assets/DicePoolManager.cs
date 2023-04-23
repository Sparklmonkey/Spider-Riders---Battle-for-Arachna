using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePoolManager : MonoBehaviour
{
    public bool IsAllStillAnimating { get { if (_diceManagers == null) { return false; } else { return _diceManagers.FindAll(x => x.IsAnimating).Count > 0; } } }
    private Transform _redDieDestination;
    private List<DiceManager> _redDiceManagers;
    private List<DiceManager> _diceManagers;

    public void AddRedDieToList(DiceManager diceManager)
    {
        _diceManagers.Add(diceManager);
    }

    public int DiceCount;

    public void ClearDice()
    {
        foreach (var dice in _diceManagers)
        {
            if(dice != null)
            {
                Destroy(dice.gameObject);
            }
        }
        _diceManagers.Clear();
        _diceManagers.Clear();
    }
    private void Awake()
    {
            _dicePrefab = Resources.Load<GameObject>("Prefabs/Dice");
    }

    private GameObject _dicePrefab;
    private Vector2 _startPoint = new Vector2(-1.3f, 0.87f);
    private float _diff = 0.2f;

    public IEnumerator RollDiceForTurn(int atkValue, Transform redDieDestination)
    {
        _diceManagers = new List<DiceManager>();
        _redDiceManagers = new List<DiceManager>();
        _redDieDestination = redDieDestination;
        int rowCount = 0;
        int columnCount = 0;
        for (int i = 0; i < atkValue; i++)
        {
            DiceManager dice = Instantiate(_dicePrefab, transform).GetComponent<DiceManager>();
            DiceFace diceResult = (DiceFace)Random.Range(0, 5);
            dice.StartDiceRoll(diceResult, new Vector3(_startPoint.x + (columnCount * _diff), _startPoint.y - (_diff * rowCount)));
            if (diceResult.Equals(DiceFace.Red))
            {
                _redDiceManagers.Add(dice);
            }
            else
            {
                _diceManagers.Add(dice);
            }

            columnCount++;
            if(columnCount == 14)
            {
                columnCount = 0;
                rowCount++;
            }
            yield return null;
        }

        while(IsAllStillAnimating)
        {
            yield return null;
        }
        StartCoroutine(RemoveWhiteDie());
    }

    private IEnumerator RemoveWhiteDie()
    {
        var diceList = new List<DiceManager>();
        float cumalativeLength = 0;
        foreach (var dice in _diceManagers)
        {
            if (dice.DiceResult.Equals(DiceFace.White))
            {
                StartCoroutine(dice.PlayDissapearAnim());
                cumalativeLength = dice.DissapearingAnimLength;
            }
            else
            {
                diceList.Add(dice);
            }
        }
        yield return new WaitForSeconds(cumalativeLength);

        _diceManagers = diceList;
        if (_redDiceManagers.Count > 0)
        {
            StartCoroutine(MoveRedDice());
        }
    }


    private IEnumerator MoveRedDice()
    {
        foreach (var dice in _redDiceManagers)
        {
            while (Vector3.Distance(dice.transform.position, _redDieDestination.position) > 0.01f)
            {
                dice.transform.position = Vector3.MoveTowards(dice.transform.position, _redDieDestination.position, Time.deltaTime * 5f);
                yield return null;
            }
            BattleManager.Instance.AddRedDie();
            Destroy(dice.gameObject);
        }
    }


    private IEnumerator RollDiceForTurnCoroutine(int atkValue)
    {
        int redDiceCount = 0;
        int rowCount = 0;
        int columnCount = 0;
        for (int i = 0; i < atkValue; i++)
        {
            DiceManager dice = Instantiate(_dicePrefab, transform).GetComponent<DiceManager>();
            DiceFace diceResult = (DiceFace)Random.Range(0, 5);
            dice.StartDiceRoll(diceResult, new Vector3(_startPoint.x + (columnCount * _diff), _startPoint.y - (_diff * rowCount)));
            if (diceResult.Equals(DiceFace.Red))
            {
                redDiceCount++;
            }

            columnCount++;
            if (columnCount == 14)
            {
                columnCount = 0;
                rowCount++;
            }
            yield return new WaitForSeconds(0.1f);
        }

        while (_diceManagers.FindAll(x => x.IsAnimating).Count > 0)
        {
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(MoveRedDice());
    }

}
