using System.Collections;
using UnityEngine;

public class Materialize : MonoBehaviour
{
    [SerializeField] private bool shouldAppear;
    [SerializeField] private float fadeStep = 0.1f;
    private MeshRenderer _meshRenderer;
    private float _colorAlpha;
    private Material _material;


    // Start is called before the first frame update
    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        SetShader();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartFade();
    }

    private void SetAlphaColor()
    {
        var color = _meshRenderer.material.color;
        color.a = 0f;
        if (shouldAppear) _meshRenderer.material.color = color;
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
    
    private IEnumerator FadeOutObject()
    {
        var color = _meshRenderer.material.color;
        
        while (color.a > 0)
        {
            color.a -= fadeStep;
            _meshRenderer.materials[0].color = color;
            yield return new WaitForEndOfFrame();
        }
        
        yield return new WaitUntil(() => _meshRenderer.material.color.a <= 0f);
    }
    
    private IEnumerator FadeInObject()
    {
        var color = _meshRenderer.material.color;
        
        while (color.a <= 1)
        {
            color.a += fadeStep;
            _meshRenderer.materials[0].color = color;
            yield return new WaitForEndOfFrame();
        }
        
        yield return new WaitUntil(() => _meshRenderer.material.color.a >= 1f);
    }
    
    public void StartFade()
    {
        StartCoroutine(shouldAppear ? nameof(FadeInObject) : nameof(FadeOutObject));
    }
}
