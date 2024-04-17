using System.Collections;
using System.Collections.Generic;
using ProjectBPop.Interfaces;
using UnityEngine;

public class ColorChanager : MonoBehaviour, IInteractable
{
    private MeshRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeColor()
    {
        _renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }

    public void Interact()
    {
        ChangeColor();
    }
}
