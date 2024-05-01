using ProjectBPop.Interfaces;
using UnityEngine;

public class InvisibleObject : Mechanism
{
    private PlayerInteract _playerReference;
    private Renderer _renderer;
    private MeshCollider _meshCollider;

    private void Awake()
    {
        _renderer = GetComponentInChildren<MeshRenderer>();
        _meshCollider = GetComponentInChildren<MeshCollider>();
    }
    
    private void OnDisable()
    {
        _playerReference.OnMagicVisionStart -= ShowMagicObject;
        _playerReference.OnMagicVisionEnd -= HideMagicObject;
    }

    private void Start()
    {
        _playerReference = GameManager.Instance.GetPlayer().GetComponent<PlayerInteract>();
        _playerReference.OnMagicVisionStart += ShowMagicObject;
        _playerReference.OnMagicVisionEnd += HideMagicObject;
        HideMagicObject();
    }

    private void ShowMagicObject()
    {
        _renderer.enabled = true;
    }

    private void HideMagicObject()
    {
        _renderer.enabled = false;
        _meshCollider.enabled = false;
    }

    private void EnableMagicObject()
    {
        _meshCollider.enabled = true;
    }

    private void DisableMagicObject()
    {
        _meshCollider.enabled = false;
    }

    public override void Activate()
    {
        ShowMagicObject();
        EnableMagicObject();
    }

    public override void Deactivate()
    {
        DisableMagicObject();
    }
}
