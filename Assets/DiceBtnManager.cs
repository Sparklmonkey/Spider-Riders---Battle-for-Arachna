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
        if (BattleManager.Instance.IsPlayerTurn)
        {
            _animator.SetTrigger("Click");
        }
    }

    private void OnMouseUp()
    {
        if (BattleManager.Instance.IsPlayerTurn)
        {
            _animator.SetTrigger("Idle");
            BattleManager.Instance.RollDice();
        }
    }

    private void OnMouseEnter()
    {
        if (BattleManager.Instance.IsPlayerTurn)
        {
            _animator.SetTrigger("Hover");
        }
    }

    private void OnMouseExit()
    {
        if (BattleManager.Instance.IsPlayerTurn)
        {
            _animator.SetTrigger("Idle");
        }
    }
}
