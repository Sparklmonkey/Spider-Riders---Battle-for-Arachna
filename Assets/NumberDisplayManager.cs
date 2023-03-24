using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NumberDisplayManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _singleDigit, _doubleDigitOne, _doubleDigitTwo, _tripleDigitOne, _tripleDigitTwo, _tripleDigitThree;

    [SerializeField]
    private List<Sprite> _numberSpriteList;
    private int _numberOnDisplay;
    public int NumberOnDisplay { get { return _numberOnDisplay; } }
    public void DisplayNumber(int number)
    {
        _numberOnDisplay = number;
        int[] result = number.ToString().Select(o => Convert.ToInt32(o) - 48).ToArray();
        SetAllInactive();
        if (result.Length == 1)
        {
            _singleDigit.gameObject.SetActive(true);
            _singleDigit.sprite = _numberSpriteList[number];
        }
        else if(result.Length == 2)
        {
            _doubleDigitOne.gameObject.SetActive(true);
            _doubleDigitTwo.gameObject.SetActive(true);
            _doubleDigitOne.sprite = _numberSpriteList[result[0]];
            _doubleDigitTwo.sprite = _numberSpriteList[result[1]];
        }
        else if(result.Length == 3)
        {
            _tripleDigitOne.gameObject.SetActive(true);
            _tripleDigitTwo.gameObject.SetActive(true);
            _tripleDigitThree.gameObject.SetActive(true);
            _tripleDigitOne.sprite = _numberSpriteList[result[0]];
            _tripleDigitTwo.sprite = _numberSpriteList[result[1]];
            _tripleDigitThree.sprite = _numberSpriteList[result[2]];
        }
        else
        {
            _tripleDigitOne.gameObject.SetActive(true);
            _tripleDigitTwo.gameObject.SetActive(true);
            _tripleDigitThree.gameObject.SetActive(true);
            _tripleDigitOne.sprite = _numberSpriteList[9];
            _tripleDigitTwo.sprite = _numberSpriteList[9];
            _tripleDigitThree.sprite = _numberSpriteList[9];
        }
    }

    public IEnumerator MoveDamageText(int number)
    {
        DisplayNumber(number);
        for (int i = 0; i < 20; i++)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, Time.deltaTime * 10f);
            yield return null;
        }
        Destroy(gameObject);
    }
    private void SetAllInactive()
    {
        _singleDigit.gameObject.SetActive(false);
        _doubleDigitOne.gameObject.SetActive(false);
        _doubleDigitTwo.gameObject.SetActive(false);
        _tripleDigitOne.gameObject.SetActive(false);
        _tripleDigitTwo.gameObject.SetActive(false);
        _tripleDigitThree.gameObject.SetActive(false);
    }
}
