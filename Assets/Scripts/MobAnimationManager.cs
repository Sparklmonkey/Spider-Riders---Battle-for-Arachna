using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAnimationManager : MonoBehaviour
{

    private List<Sprite> animationSpriteList;

    private bool _loop;
    private bool _canStartAnimating;
    private float _frameSeconds = 1;
    private string _mobName;
    private SpriteRenderer _spriteRenderer;
    private int _frame = 0;
    private float _deltaTime = 0f;
    private float _animLength = 0f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.transform.localScale = new(1f, 1f, 1f);
    }

    public void SetupManager(string mobName)
    {
        if (mobName.Contains("Door"))
        {
            _mobName = "Door";
        }
        else
        {
            _mobName = mobName;
        }
        _canStartAnimating = true;
        PlayIdleAnimation();
    }

    private IEnumerator PlayAnimation(bool isLoop)
    {
        float time = 0f;
        _frame = 0;
        while (_frame < animationSpriteList.Count - 1)
        {
            time += Time.deltaTime;
            if(time >= _frameSeconds)
            {
                time -= _frameSeconds;
                _frame++;
                _spriteRenderer.sprite = animationSpriteList.Find(x => x.name == _frame.ToString());
            }
            yield return null;
        }
        if (isLoop)
        {
            StartCoroutine(PlayAnimation(isLoop));
        }
    }

    private void LoadAnimationSprites(string animName)
    {
        _loop = false;
        _frame = 1;
        animationSpriteList = new List<Sprite>(Resources.LoadAll<Sprite>($"Enemies/{_mobName}/{animName}"));
        _animLength = animationSpriteList.Count / 24.0f;
        _frameSeconds = _animLength / 24.0f;
    }

    public void PlayIdleAnimation()
    {
        LoadAnimationSprites("Idle");
        StartCoroutine(PlayAnimation(true));
    }

    public IEnumerator PlayPlayerAttackAnim()
    {
        LoadAnimationSprites("Hit");
        yield return new WaitForSeconds(_animLength);
        PlayIdleAnimation();
    }

    public IEnumerator PlayMobAttackAnim()
    {
        LoadAnimationSprites("Attack");
        yield return new WaitForSeconds(_animLength);
        PlayIdleAnimation();
    }

    public IEnumerator PlayTakeDamageAnim()
    {
        LoadAnimationSprites("TakeDamage");
        yield return new WaitForSeconds(_animLength);
        PlayIdleAnimation();
    }

    public IEnumerator PlayDeathAnim()
    {
        StopAllCoroutines();
        LoadAnimationSprites("Death");
        yield return StartCoroutine(PlayAnimation(false));
    }

}
