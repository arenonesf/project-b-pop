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
    public SourceType PlayerMagicSourceType { get; private set; }

    private void OnEnable()
    {
        inputReader.PlayerGrabMagicEvent += TryGrabMagic;
        inputReader.PlayerFireMagicEvent += TrySendMagic;
        inputReader.PlayerInteractEvent += TryInteract;
    }

    private void OnDisable()
    {
        inputReader.PlayerGrabMagicEvent -= TryGrabMagic;
        inputReader.PlayerFireMagicEvent -= TrySendMagic;
        inputReader.PlayerInteractEvent -= TryInteract;
    }

    private void Awake()
    {
        _playerCameraTransform = GetComponentInChildren<Camera>().transform;
        PlayerMagicSourceType = SourceType.None;
    }

    private void FixedUpdate()
    {
        var ray = new Ray(_playerCameraTransform.position, _playerCameraTransform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
    }

    private void TryInteract()
    {
        if (PlayerMagicSourceType != SourceType.None) return;
        if (!Physics.Raycast(_playerCameraTransform.position, _playerCameraTransform.forward, out var hit, rayDistance,
                interactionLayer.value)) return;
        hit.transform.GetComponent<IInteractable>().Interact();
    }
    
    #region Magic
    private void TryGrabMagic()
    {
        if (PlayerMagicSourceType != SourceType.None) return;
        if (!Physics.Raycast(_playerCameraTransform.position, _playerCameraTransform.forward, out var hit, rayDistance,
                interactionMagicLayer.value)) return;
        hit.transform.GetComponent<IInteractable>().Interact();
    }

    private void TrySendMagic()
    {
        if (PlayerMagicSourceType == SourceType.None) return;
        if (!Physics.Raycast(_playerCameraTransform.position, _playerCameraTransform.forward, out var hit, rayDistance,
                interactionMagicLayer.value)) return;
        hit.transform.GetComponent<IInteractable>().Interact();
    }

    public void SetMagicType(SourceType source)
    {
        PlayerMagicSourceType = source;
        Debug.Log(PlayerMagicSourceType);
    }
    #endregion
}
