using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BlackboardChangeEmision : MonoBehaviour
{
    [SerializeField] private new Renderer renderer;
    public Material Material => renderer.material;
    public Color EmissionColorValue;
    public float Intensity;
    public float ActivateSpeed;
    public float DeactivateSpeed;
    public float MinDeactivatedIdleValue;
    public float MinActivatedIdleValue;
}
