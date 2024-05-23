using System.Collections;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private float step = 0.01f;

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
}
