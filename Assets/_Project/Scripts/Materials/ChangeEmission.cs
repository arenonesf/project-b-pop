using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEmission : MonoBehaviour
{
    [SerializeField] private Renderer renderer;
    private Material _material;
    private Color _emissionColorValue;
    private float _intensity;

    private void Start()
    {
        _material = renderer.material;
        _emissionColorValue = _material.color;
    }

    private void Update()
    {
        ActivateEmission();
    }

    private void ActivateEmission()
    {
        _intensity = Mathf.Lerp(_intensity, 1, Time.deltaTime * 2f);
        Mathf.Round(_intensity);
        Debug.Log(_intensity);
        _material.SetVector("_EmissionColor", _emissionColorValue * _intensity);
    }

    private void DeactivateEmission()
    {
        _intensity = Mathf.Lerp(1, 0, Time.deltaTime * 2f);
        Debug.Log(_intensity);
        _material.SetVector("_EmissionColor", _emissionColorValue * _intensity);
    }
}
