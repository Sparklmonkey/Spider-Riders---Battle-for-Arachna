using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackBtnManager : MonoBehaviour
{

    [SerializeField]
    private BattleManager _battleManager;
    private bool _canAnimate = false;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _battleManager.OnChangeTurn += OnTurnChange;
    }
    private void OnTurnChange(object sender, BattleManager.BattleManagerEventArgs e)
    {
        _canAnimate = e.IsPlayerTurn;
    }

    private void OnMouseDown()
        {
            if (_canAnimate)
            {
                _animator.SetTrigger("Hover");
            }
        }

    private void OnMouseUp()
    {
        if (_canAnimate)
        {
            _animator.SetTrigger("Idle");
            StartCoroutine(_battleManager.AttackOpponent());
        }
    }

    private void OnMouseEnter()
    {
        if (_canAnimate)
        {
            _animator.SetTrigger("Hover");
        }
    }

    private void OnMouseExit()
    {
        if (_canAnimate)
        {
            _animator.SetTrigger("Idle");
        }
    }
}
