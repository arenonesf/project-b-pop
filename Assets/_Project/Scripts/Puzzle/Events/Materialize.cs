using ProjectBPop.Interfaces;
using UnityEngine;

public class Materialize : MonoBehaviour, IReact
{ 
    private MeshRenderer _meshRenderer;
    private float _colorAlpha;
    private Material _material;
    private BoxCollider _collider;
    
    // Start is called before the first frame update
    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<BoxCollider>();
        var color = _meshRenderer.materials[0].color;
        color.a = .4f;
        _meshRenderer.materials[0].color = color;
    }
    
    public void React(bool solve)
    {
        if (solve)
        {
            _meshRenderer.enabled = true;
            var color = _meshRenderer.material.color;
            color.a = 1f;
            _meshRenderer.material.color = color;
            _collider.isTrigger = false;
        }
        else
        {
            _meshRenderer.enabled = false;
            var color = _meshRenderer.material.color;
            color.a = .4f;
            _meshRenderer.material.color = color;
            _collider.isTrigger = true;
        }
    }
}
