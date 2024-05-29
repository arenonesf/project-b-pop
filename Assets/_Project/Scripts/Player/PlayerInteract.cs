using ProjectBPop.Input;
using ProjectBPop.Interfaces;
using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private LayerMask interactionMagicLayer;
    [SerializeField] private SkinnedMeshRenderer runicArm;
    [SerializeField] private GameObject redOrb;
    [SerializeField] private GameObject blueOrb;
    [SerializeField] private GameObject greenOrb;
    [SerializeField] private HandAnimationStateController handController;
    public bool Interacting;
    
    private Transform _playerCameraTransform;
    public SourceType PlayerMagicSourceType { get; private set; }
    public event Action<SourceType> OnMagicChangeColor;
    
    private void OnEnable()
    {
        inputReader.PlayerMagicInteractionEvent += TryMagicInteraction;
        inputReader.PlayerInteractEvent += TryInteract;
        handController.PlayerSendMagic += HideHandMesh;
    }

    private void OnDisable()
    {
        inputReader.PlayerMagicInteractionEvent -= TryMagicInteraction;
        inputReader.PlayerInteractEvent -= TryInteract;
        handController.PlayerSendMagic -= HideHandMesh;
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
        if (Interacting) return;
        hit.transform.GetComponent<IInteractable>().Interact();
    }

    public void SetMagicType(SourceType source)
    {
        PlayerMagicSourceType = source;
        runicArm.enabled = true;
        
        if (PlayerMagicSourceType != SourceType.None)
        {
            handController.ShowHand();
        }
        else
        {
            handController.SendMagic();
        }
        
        OnMagicChangeColor?.Invoke(PlayerMagicSourceType);
    }

    private void HideHandMesh()
    {
        runicArm.enabled = false;
    }
    #endregion

    public void ToggleOrbs()
    {
        redOrb.SetActive(PlayerMagicSourceType == SourceType.Red);
        blueOrb.SetActive(PlayerMagicSourceType == SourceType.Blue);
        greenOrb.SetActive(PlayerMagicSourceType == SourceType.Green);
    }
}
