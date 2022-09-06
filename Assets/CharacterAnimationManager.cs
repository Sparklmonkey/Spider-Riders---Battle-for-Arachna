using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterAnimationManager : MonoBehaviour
{
    private Animator animator;
    private bool isIdle = true;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        if(SceneManager.GetActiveScene().name == "CharacterSheet")
        {
            StartCoroutine("IdleAnimationRoutine");
        }
    }

    private IEnumerator IdleAnimationRoutine()
    {
        while (isIdle)
        {
            yield return new WaitForSeconds(30f);

            if(Random.Range(0,2) == 0)
            {
                animator.SetTrigger("Idle");
            }
        }
    }
}
