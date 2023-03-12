using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBtnManager : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnMouseDown()
    {
        if (BattleManagerNew.Instance.IsPlayerTurn)
        {
            _animator.SetTrigger("Click");
        }
    }

    private void OnMouseUp()
    {
        if (BattleManagerNew.Instance.IsPlayerTurn)
        {
            _animator.SetTrigger("Idle");
            BattleManagerNew.Instance.RollDice();
        }
    }

    private void OnMouseEnter()
    {
        if (BattleManagerNew.Instance.IsPlayerTurn)
        {
            _animator.SetTrigger("Hover");
        }
    }

    private void OnMouseExit()
    {
        if (BattleManagerNew.Instance.IsPlayerTurn)
        {
            _animator.SetTrigger("Idle");
        }
    }
}
