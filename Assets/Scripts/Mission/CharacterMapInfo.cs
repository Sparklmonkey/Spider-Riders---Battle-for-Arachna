using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMapInfo : MonoBehaviour
{
    private Animator _animator;
    public OverlayTile CurrentTile;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TriggerRunAnim()
    {
        _animator.SetTrigger("Run");
    }

    public void StopRunAnim()
    {
        _animator.SetTrigger("Stop");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Player Interaction with {collision.gameObject.name}");
        var interactable = collision.gameObject.GetComponent<IInteractable>();
        if (interactable == null) { return; }
        interactable.OnInteract();
    }
}
