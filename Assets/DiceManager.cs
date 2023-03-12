using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    private Animator _animator;
    public DiceFace DiceResult;

    private List<string> _diceAnimQueue;
    public bool IsAnimating;

    private Vector3 _originalPosition;
    private bool _isDragging;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsAnimating) { return; }
        if (_isDragging)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        }
        else if(transform.position != _originalPosition)
        {
            transform.position = _originalPosition;
        }
    }
    private void OnMouseDown()
    {
        if(DiceResult == DiceFace.White || IsAnimating) { return; }
        _isDragging = true;
    }

    private void OnMouseUp()
    {
        _isDragging = false;
    }

    public void StartDiceRoll(DiceFace result, Vector3 position)
    {
        transform.localPosition = position;
        _originalPosition = transform.position;
        _diceAnimQueue = new List<string> { "DiceRollStart", $"Result{result}" };
        DiceResult = result;
        if (result == DiceFace.White)
        {
            _diceAnimQueue.Add("ResultWhite");
            _diceAnimQueue.Add("DiceDisappearAnim");
        }
        StartCoroutine(PlayDiceAnimQueue());
    }


    private IEnumerator PlayDiceAnimQueue()
    {
        IsAnimating = true;
        foreach (var animClip in _diceAnimQueue)
        {
            _animator.Play(animClip);
            while (_animator.GetCurrentAnimatorStateInfo(0).IsName(animClip))
            {
                yield return null;
            }
        }
        IsAnimating = false;
    }
}

public enum DiceFace
{
    Red,
    Orange,
    Blue,
    White,
    Green
}
