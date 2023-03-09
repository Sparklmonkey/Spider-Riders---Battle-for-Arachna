using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBtnManager : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnMouseDown()
    {
        _animator.SetTrigger("Hover");
    }

    private void OnMouseUp()
    {
        _animator.SetTrigger("Idle");
        //Do Damage Calculation
    }

    private void OnMouseEnter()
    {
        _animator.SetTrigger("Hover");
    }

    private void OnMouseExit()
    {
        _animator.SetTrigger("Idle");
    }
}
