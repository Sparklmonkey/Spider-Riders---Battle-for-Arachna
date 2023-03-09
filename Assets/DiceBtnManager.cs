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
        _animator.SetTrigger("Click");
    }

    private void OnMouseUp()
    {
        _animator.SetTrigger("Idle");
        //Roll Dice
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
