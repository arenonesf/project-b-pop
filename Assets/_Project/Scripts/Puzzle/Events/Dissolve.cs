using System.Collections;
using ProjectBPop.Interfaces;
using UnityEngine;

public class Dissolve : Mechanism
{
    [SerializeField] private Material material;
    [SerializeField] private float step = 0.01f;
    [SerializeField] private bool shouldAppear;

    public void StartDissolving()
    {
        StartCoroutine(Dissapear());
    }

    public void StartAppearing()
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
    }
    
    private IEnumerator Appear()
    {
        var miniStep = 1f;
        while (miniStep > 0f)
        {
            material.SetFloat("_Dissolve", miniStep);
            miniStep -= step;
            yield return null;
        }
        
        material.SetFloat("_Dissolve", 0f);
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
