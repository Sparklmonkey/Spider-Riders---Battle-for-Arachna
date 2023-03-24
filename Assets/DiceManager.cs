using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public DiceFace DiceResult;
    public bool IsAnimating = true;
    public float DissapearingAnimLength { get { return _dissapearClip.length; } }
    private Vector3 _originalPosition;
    private bool _isDragging;
    private void Update()
    {
        if (IsAnimating || DiceResult.Equals(DiceFace.Red)) { return; }
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
        DiceResult = result;
        _clipsToPlay = _baseClips;
        _clipsToPlay.Add(_endResults[(int)result]);
        PlayClips();
    }

    public Animator animator;
    [SerializeField]
    private List<AnimationClip> _baseClips, _endResults;
    [SerializeField]
    private AnimationClip _dissapearClip;
    private List<AnimationClip> _clipsToPlay;

    public void PlayClips()
    {
        float cumulativeLength = 0;
        foreach (AnimationClip clip in _clipsToPlay)
         {
            StartCoroutine(PlayClip(clip.name, cumulativeLength));
            cumulativeLength += clip.length;
        }
        StartCoroutine(SetAnimationBool(cumulativeLength));
    }

    private IEnumerator SetAnimationBool(float startTime)
    {
        yield return new WaitForSeconds(startTime);
        IsAnimating = false;
    }

    public IEnumerator PlayDissapearAnim()
    {
        IsAnimating = true;
        animator.Play("DiceDisappearAnim");
        yield return new WaitForSeconds(_dissapearClip.length);
        IsAnimating = false;
        Destroy(gameObject);
    }

    private IEnumerator PlayClip(string clipName, float startTime)
    {
        yield return new WaitForSeconds(startTime);
        // Remeber to set you Animator Animation state name same as the Clip name! 
        animator.Play(clipName);
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
