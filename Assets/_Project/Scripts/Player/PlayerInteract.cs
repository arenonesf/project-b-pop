using ProjectBPop.Input;
using ProjectBPop.Magic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private LayerMask interactionMagicLayer;
    private Transform _playerCameraTransform;
    private SourceType _playerMagicSourceType;

    public SourceType PlayerMagicSourceType
    {
        get => _playerMagicSourceType;
        private set => _playerMagicSourceType = value;
    }

    private void OnEnable()
    {
        inputReader.PlayerGrabMagicEvent += TryGrabMagic;
    }

    private void OnDisable()
    {
        inputReader.PlayerGrabMagicEvent -= TryGrabMagic;
    }

    private void Awake()
    {
        _playerCameraTransform = GetComponentInChildren<Camera>().transform;
        _playerMagicSourceType = SourceType.None;
    }

    private void FixedUpdate()
    {
        var ray = new Ray(_playerCameraTransform.position, _playerCameraTransform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
        
    }

    private void TryGrabMagic()
    {
        if (!Physics.Raycast(_playerCameraTransform.position, _playerCameraTransform.forward, out var hit, rayDistance,
                interactionMagicLayer.value)) return;
        hit.transform.GetComponent<IInteractable>().Interact();
    }

    public void SetMagicType(SourceType source)
    {
        _playerMagicSourceType = source;
        Debug.Log(_playerMagicSourceType);
    }
}
