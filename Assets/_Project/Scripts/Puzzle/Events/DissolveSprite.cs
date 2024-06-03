using System;
using System.Collections;
using ProjectBPop.Interfaces;
using UnityEngine;

public class DissolveSprite : Mechanism
{
    [SerializeField] private Material material;
    [SerializeField] private float step = 0.01f;
    [SerializeField] private bool shouldAppear;
    private SpriteRenderer _renderer;
    private static readonly int DissolveValue = Shader.PropertyToID("_DissolveAmount");

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        VisualAlignment.OnVisionCompleted += Activate;
    }

    private void OnDisable()
    {
        VisualAlignment.OnVisionCompleted -= Activate;
    }

    private void Start()
    {
        _renderer.enabled = !shouldAppear;
        material.SetFloat(DissolveValue, shouldAppear ? 1f : 0f);
    }

    private void StartDissolving()
    {
        StartCoroutine(Disappear());
    }

    private void StartAppearing()
    {
        StartCoroutine(Appear());
    }

    private IEnumerator Disappear()
    {
        var miniStep = 0.293f;
        while (miniStep < 1f)
        {
            material.SetFloat(DissolveValue, miniStep);
            miniStep += 2 * step;
            yield return null;
        }
        
        material.SetFloat(DissolveValue, 1f);
        _renderer.enabled = false;
    }
    
    private IEnumerator Appear()
    {
        _renderer.enabled = true;
        var miniStep = 1f;
        while (miniStep > 0f)
        {
            material.SetFloat(DissolveValue, miniStep);
            miniStep -= step;
            yield return null;
        }
        
        material.SetFloat(DissolveValue, 0f);
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
