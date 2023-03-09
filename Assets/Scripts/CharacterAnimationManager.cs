using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterAnimationManager : MonoBehaviour
{
    private Animator _animator;
    private bool _isIdle = true;
    private void Awake()
    {
        //if(SceneManager.GetActiveScene().name == "CharacterSheet")
        //{
        //    StartCoroutine("IdleAnimationRoutine");
        //}
    }

    public void TriggerRunAnim()
    {
        _animator.SetTrigger("Run");
    }

    public void StopRunAnim()
    {
        _animator.SetTrigger("Idle");
    }
    private IEnumerator IdleAnimationRoutine()
    {
        while (_isIdle)
        {
            yield return new WaitForSeconds(30f);

            if(Random.Range(0,2) == 0)
            {
                _animator.SetTrigger("Idle");
            }
        }
    }
}
