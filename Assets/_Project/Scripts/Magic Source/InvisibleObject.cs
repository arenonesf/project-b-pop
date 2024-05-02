using ProjectBPop.Interfaces;
using UnityEngine;

public class InvisibleObject : Mechanism
{
    private PlayerInteract _playerReference;
    private Renderer _renderer;
    private MeshCollider _meshCollider;
    private bool _solved;

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
        Debug.Log(_solved);
    }

    private void ShowMagicObject()
    {
        if (!_solved)
        {
            _renderer.enabled = true;
        }
    }

    private void HideMagicObject()
    {
        if (!_solved)
        {
            _renderer.enabled = false;
            _meshCollider.enabled = false;
        }
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
        _solved = true;
    }

    public override void Deactivate()
    {
        DisableMagicObject();
        _solved = false;
    }
}
