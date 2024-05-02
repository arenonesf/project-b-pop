using UnityEngine;

public class DecalObject : MonoBehaviour
{
    private PlayerInteract _playerReference;
    private MeshRenderer _meshRenderer;
    [SerializeField] private GameObject decal;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _playerReference = GameManager.Instance.GetPlayer().GetComponent<PlayerInteract>();
        _playerReference.OnMagicVisionStart += ShowDecal;
        _playerReference.OnMagicVisionEnd += HideDecal;
        HideDecal();
    }

    private void OnDisable()
    {
        _playerReference.OnMagicVisionStart -= ShowDecal;
        _playerReference.OnMagicVisionEnd -= HideDecal;
    }

    private void ShowDecal()
    {
        _meshRenderer.material.EnableKeyword("_EMISSION");
        decal.SetActive(true);
    }

    private void HideDecal()
    {
        _meshRenderer.material.DisableKeyword("_EMISSION");
        decal.SetActive(false);
    }
}
