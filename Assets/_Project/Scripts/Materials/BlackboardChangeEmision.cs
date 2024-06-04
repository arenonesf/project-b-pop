using System.Collections.Generic;
using UnityEngine;

public class BlackboardChangeEmision : MonoBehaviour
{
    public List<Renderer> Renderers;
    public List <Material> Materials;
    public Color EmissionColorValue;
    public float Intensity;
    public float Alpha;
    public float MinIntensity;
    public float MaxIntensity;
    public float ActivateOpacitySpeed;
    public float DeactivateOpacitySpeed;
    public float ActivateEmisionSpeed;
    public float DeactivateEmisionSpeed;
    public float MinDeactivatedIdleOpacityValue;
    public float MinActivatedIdleOpacityValue;
    public float MinDeactivatedIdleEmisionValue;
    public float MinActivatedIdleEmisionValue;

    private void Awake()
    {
        foreach (var renderer in Renderers)
        {
            Materials.Add(renderer.material);
        }
    }
}
