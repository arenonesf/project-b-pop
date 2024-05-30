using System;
using System.Collections;
using ProjectBPop.Interfaces;
using UnityEngine;

public class Dissolve : Mechanism
{
    [SerializeField] private Material material;
    [SerializeField] private float step = 0.01f;
    [SerializeField] private bool shouldAppear;
    private MeshRenderer _renderer;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        if (shouldAppear)
        {
            _renderer.enabled = false;
            _boxCollider.enabled = false;
        }
    }

    private void StartDissolving()
    {
        StartCoroutine(Dissapear());
    }

    private void StartAppearing()
    {
        StartCoroutine(Appear());
    }

    private IEnumerator Dissapear()
    {
        var miniStep = 0f;
        while (miniStep < 1f)
        {
            material.SetFloat("_Dissolve", miniStep);
            miniStep += step;
            yield return null;
        }
        
        material.SetFloat("_Dissolve", 1f);
        _boxCollider.enabled = false;
        _renderer.enabled = false;
    }
    
    private IEnumerator Appear()
    {
        _renderer.enabled = true;
        var miniStep = 1f;
        while (miniStep > 0f)
        {
            material.SetFloat("_Dissolve", miniStep);
            miniStep -= step;
            yield return null;
        }
        
        material.SetFloat("_Dissolve", 0f);
        _boxCollider.enabled = true;
    }
    
    public override void Activate()
    {
        if (shouldAppear)
        {
            StartAppearing();
        }
        else
        {
            StartDissolving();
        }
    }

    public override void Deactivate()
    {
        if (shouldAppear)
        {
            StartDissolving();
        }
        else
        {
            StartAppearing();
        }
    }
}
