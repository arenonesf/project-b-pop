using UnityEngine;

public class BlackboardChangeEmision : MonoBehaviour
{
    public Renderer Renderer;
    public Material Material => Renderer.material;
    public Color EmissionColorValue;
    public float Intensity;
    public float MinIntensity;
    public float ActivateOpacitySpeed;
    public float DeactivateOpacitySpeed;
    public float ActivateEmisionSpeed;
    public float DeactivateEmisionSpeed;
    public float MinDeactivatedIdleOpacityValue;
    public float MinActivatedIdleOpacityValue;
    public float MinDeactivatedIdleEmisionValue;
    public float MinActivatedIdleEmisionValue;
}
