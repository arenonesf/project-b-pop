using System;
using UnityEngine;

public class HandAnimationStateController : MonoBehaviour
{
    private Animator _animator;
    private int _isPickingMagicHash;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _isPickingMagicHash = Animator.StringToHash("isPickingMagic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
