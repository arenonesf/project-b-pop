using System.Collections;
using ProjectBPop.Interfaces;
using UnityEngine;

public class DissolveMeshMechanism : Mechanism
{
    [SerializeField] private Material[] materials;
    [SerializeField] private float step = 0.01f;
    [SerializeField] private bool shouldAppear;
    private MeshCollider _meshCollider;
    private static readonly int DissolveValue = Shader.PropertyToID("_Amount");

    private void Awake()
    {
        _meshCollider = GetComponentInChildren<MeshCollider>();
    }

    private void Start()
    {
        if (!shouldAppear) return;
        _meshCollider.enabled = false;
        foreach (var material in materials)
        {
            material.SetFloat(DissolveValue, 1f);
        }
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
        var miniStep = 0f;
        while (miniStep < 1f)
        {
            foreach (var material in materials)
            {
                material.SetFloat(DissolveValue, miniStep);
            }
            miniStep += step;
            yield return null;
        }
        
        foreach (var material in materials)
        {
            material.SetFloat(DissolveValue, 1f);
        }
        _meshCollider.enabled = false;
    }
    
    private IEnumerator Appear()
    {
        var miniStep = 1f;
        while (miniStep > 0f)
        {
            foreach (var material in materials)
            {
                material.SetFloat(DissolveValue, miniStep);
            }
            miniStep -= step;
            yield return null;
        }
        
        foreach (var material in materials)
        {
            material.SetFloat(DissolveValue, 0f);
        }
        _meshCollider.enabled = true;
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
