using UnityEngine;
using UnityEngine.InputSystem;

public class MaterialEmissionChanger : MonoBehaviour
{
    private Material _material;
    private bool _emissionEnabled;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _emissionEnabled = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Keyboard.current[Key.E].wasPressedThisFrame) return;
        if (!_emissionEnabled)
        {
            EnableEmission();
        }
        else
        {
            DisableEmission();
        }
    }

    private void DisableEmission()
    {
        _material.DisableKeyword("_EMISSION");
        _emissionEnabled = false;
    }

    private void EnableEmission()
    {
        _material.EnableKeyword("_EMISSION");
        _emissionEnabled = true;
    }
}
