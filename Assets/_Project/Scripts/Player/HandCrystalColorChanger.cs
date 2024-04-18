using ProjectBPop.Magic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class HandCrystalColorChanger : MonoBehaviour
{   
    [SerializeField]private Material material;
    [SerializeField] private Color noneColor;
    [SerializeField] private Color redColor;
    [SerializeField] private Color blueColor;
    [SerializeField] private Color greenColor;
    private PlayerInteract _playerReference;


    private void OnEnable()
    {
        _playerReference.OnMagicChangeColor += ChangeMagicColor;
    }

    private void OnDisable()
    {
        _playerReference.OnMagicChangeColor -= ChangeMagicColor;
    }

    private void Awake()
    {
        _playerReference = GetComponentInParent<PlayerInteract>();
        ChangeMagicColor(SourceType.None);
    }

    private void ChangeMagicColor(SourceType source)
    {
        switch (source) 
        {
            case SourceType.Red:
                material.color = redColor;
            break;
            case SourceType.Blue:
                material.color = blueColor;
            break;
            case SourceType.Green:
                material.color = greenColor;
            break;
            default: 
                material.color = noneColor;
            break;
        }
    }
}
