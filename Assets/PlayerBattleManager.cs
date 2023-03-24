using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleManager : MonoBehaviour
{
    [SerializeField]
    private List<AnimationClip> _playerAnimations;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerAnimations = new List<AnimationClip>(_animator.runtimeAnimatorController.animationClips);
    }
    public void PlayAnimation(string animName)
    {
        _animator.Play(animName);
    }

    public void PlayIdleAnimation()
    {
        _animator.Play("Idle");
    }

    public IEnumerator PlayPlayerAttackAnim()
    {
        _animator.Play("Hit_1");
        yield return new WaitForSeconds(_playerAnimations.Find(x => x.name == "Hit_1").length);
        PlayIdleAnimation();
    }

    public IEnumerator PlayMobAttackAnim()
    {
        _animator.Play("Hit");
        yield return new WaitForSeconds(_playerAnimations.Find(x => x.name == "Hit").length);
        PlayIdleAnimation();
    }

    public IEnumerator PlayTakeDamageAnim()
    {
        _animator.Play("TakeDamage");
        yield return new WaitForSeconds(_playerAnimations.Find(x => x.name == "TakeDamage").length);
        PlayIdleAnimation();
    }

    public IEnumerator PlayDeathAnim()
    {
        _animator.Play("Death");
        yield return new WaitForSeconds(_playerAnimations.Find(x => x.name == "Death").length);
    }

    public IEnumerator PlayVictoryAnim()
    {
        _animator.Play("Victory");
        yield return new WaitForSeconds(_playerAnimations.Find(x => x.name == "Victory").length);
    }
}
