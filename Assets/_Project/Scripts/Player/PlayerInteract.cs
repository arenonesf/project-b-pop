using ProjectBPop.Input;
using ProjectBPop.Interfaces;
using ProjectBPop.Magic;
using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private LayerMask interactionMagicLayer;
    private Transform _playerCameraTransform;
    public SourceType PlayerMagicSourceType { get; private set; }
    public event Action<SourceType> OnMagicChangeColor;
    public event Action OnMagicVisionStart;
    public event Action OnMagicVisionEnd;
    
    private void OnEnable()
    {
        inputReader.PlayerMagicInteractionEvent += TryMagicInteraction;
        inputReader.PlayerInteractEvent += TryInteract;
    }

    private void OnDisable()
    {
        inputReader.PlayerMagicInteractionEvent -= TryMagicInteraction;
        inputReader.PlayerInteractEvent -= TryInteract;
    }

    private void Awake()
    {
        _playerCameraTransform = GetComponentInChildren<Camera>().transform;
        PlayerMagicSourceType = SourceType.None;
    }

    private void TryInteract()
    {
        if (PlayerMagicSourceType != SourceType.None) return;
        if (!Physics.Raycast(_playerCameraTransform.position, _playerCameraTransform.forward, out var hit, rayDistance,
                interactionLayer.value)) return;
        hit.transform.GetComponent<IInteractable>().Interact();
    }
    
    #region Magic
    private void TryMagicInteraction()
    {
        if (!Physics.Raycast(_playerCameraTransform.position, _playerCameraTransform.forward, out var hit, rayDistance,
                interactionMagicLayer.value)) return;
        hit.transform.GetComponent<IInteractable>().Interact();
    }

    public void SetMagicType(SourceType source)
    {
        PlayerMagicSourceType = source;
        if (PlayerMagicSourceType != SourceType.None)
        {
            OnMagicVisionStart?.Invoke();
        }
        else
        {
            OnMagicVisionEnd?.Invoke();
        }
        OnMagicChangeColor?.Invoke(PlayerMagicSourceType);
    }
    #endregion
}
