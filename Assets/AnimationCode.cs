using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCode : MonoBehaviour
{
    // Start is called before the first frame update
    private MagicArtifact _magicArtifact;
    private Animator _animator;
    void Start()
    {
     _magicArtifact = GetComponent<MagicArtifact>();
     _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_magicArtifact.Active) _animator.SetBool("Active", true);
        else _animator.SetBool("Active", false);
    }
}
