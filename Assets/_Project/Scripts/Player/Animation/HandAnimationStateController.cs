using System;
using ProjectBPop.Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class HandAnimationStateController : MonoBehaviour
{
    [SerializeField] private PlayerInteract playerInteract;
    private Animator _animator;
    private int _isShowingHandHash;
    private int _isSendingMagicHash;
    private int _isMagicOnHandHash;
    private int _isDoingPerspectiveHash;
    private int _random;
    private PlayerInteract _playerInteract;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInteract = GetComponentInParent<PlayerInteract>();
    }

    private void OnEnable()
    {
        VisualAlignment.OnVisionCompleted += SendMagicPerspective;
    }

    private void OnDisable()
    {
        VisualAlignment.OnVisionCompleted -= SendMagicPerspective;
    }

    private void Start()
    {
        _isShowingHandHash = Animator.StringToHash("show");
        _isSendingMagicHash = Animator.StringToHash("sendingMagic");
        _isDoingPerspectiveHash = Animator.StringToHash("perspective");
    }

    public void ToggleOrb()
    {
        playerInteract.ToggleOrbs();
    }

    public void HideHandMesh()
    {
        playerInteract.DisableRunicArm();
    }

    public void BlockInteraction()
    {
        playerInteract.Interacting = true;
    }
    
    public void AllowInteraction()
    {
        playerInteract.Interacting = false;
    }
    
    public void ShowHand()
    {
        _animator.SetTrigger(_isShowingHandHash);
    }

    public void SendMagic()
    {
        _animator.SetTrigger(_isSendingMagicHash);
    }

    private void SendMagicPerspective()
    {
        _playerInteract.EnableRunicArm();
        _animator.SetTrigger(_isDoingPerspectiveHash);
    }
}
