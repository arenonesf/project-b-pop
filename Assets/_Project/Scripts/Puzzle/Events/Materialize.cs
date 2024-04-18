using System.Collections;
using ProjectBPop.Interfaces;
using UnityEngine;

public class Materialize : MonoBehaviour, IReact
{ 
    [SerializeField] private float fadeStep = 0.1f;
    private MeshRenderer _meshRenderer;
    private float _colorAlpha;
    private Material _material;
    private BoxCollider _collider;
    private bool _hasAppeared;
    
    // Start is called before the first frame update
    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<BoxCollider>();
        var color = _meshRenderer.materials[0].color;
        color.a = .4f;
        _meshRenderer.materials[0].color = color;
    }

    private void SetShader()
    {
        _meshRenderer.material.SetFloat("_Mode", 2);
        _meshRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        _meshRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        _meshRenderer.material.SetInt("_ZWrite", 0);
        _meshRenderer.material.DisableKeyword("_ALPHATEST_ON");
        _meshRenderer.material.EnableKeyword("_ALPHABLEND_ON");
        Material material;
        (material = _meshRenderer.material).DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;
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
