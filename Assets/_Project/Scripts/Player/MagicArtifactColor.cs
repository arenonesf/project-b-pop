using ProjectBPop.Magic;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class MagicArtifactColor : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private Color redColorActivated;
    [SerializeField] private Color blueColorActivated;
    [SerializeField] private Color greenColorActivated;
    [SerializeField] private Color colorlessColorActivated;
    [SerializeField] private Color redColorDesactivated;
    [SerializeField] private Color blueColorDesactivated;
    [SerializeField] private Color greenColorDesactivated;
    [SerializeField] private Color colorlessColorDesactivated;
    [SerializeField] private GameObject artifact;
    

    private void OnEnable()
    {
        artifact.gameObject.GetComponent<MagicSource>().OnArtifactChangeColor += ChangeMagicColor;
        artifact.gameObject.GetComponent<MagicNode>().OnArtifactChangeColor += ChangeMagicColor;
    }

    private void OnDisable()
    {
        artifact.gameObject.GetComponent<MagicSource>().OnArtifactChangeColor -= ChangeMagicColor;
        artifact.gameObject.GetComponent<MagicNode>().OnArtifactChangeColor -= ChangeMagicColor;
    }

    private void Awake()
    {     
        artifact = GetComponent<GameObject>();
    }

    private void ChangeMagicColor(SourceType source, bool hasMagic)
    {
        switch (source)
        {
            case SourceType.Red:
                if (hasMagic)
                    material.color = redColorActivated;
                else
                    material.color = redColorDesactivated;
            break;
            case SourceType.Blue:
                if (hasMagic)
                    material.color = blueColorActivated;
                else
                    material.color = blueColorDesactivated;
            break;
            case SourceType.Green:
                if (hasMagic)
                    material.color = greenColorActivated;
                else
                    material.color = greenColorDesactivated;
            break;
            default:
                if (hasMagic)
                    material.color = colorlessColorActivated;
                else
                    material.color = colorlessColorDesactivated;
            break;
        }
    }
}
