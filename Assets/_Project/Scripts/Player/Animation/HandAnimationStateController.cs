using System;
using ProjectBPop.Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class HandAnimationStateController : MonoBehaviour
{
    [SerializeField] private PlayerInteract playerInteract;
    private HeadBobController _playerHeadBob;
    private Animator _animator;
    private int _isShowingHandHash;
    private int _isPickingMagicHash;
    private int _isPickingMagicVariantHash;
    private int _isSendingMagicHash;
    private int _isSendingMagicVariantHash;
    private int _isHidingHandHash;
    private int _isMagicOnHandHash;
    private int _isDoingPerspectiveHash;
    private int _random;
    public Action PlayerSendMagic;
    private PlayerMovement _playerMovement;
    private PlayerLook _playerLook;
    private PlayerInteract _playerInteract;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponentInParent<PlayerMovement>();
        _playerHeadBob = GetComponentInParent<HeadBobController>();
        _playerLook = GetComponentInParent<PlayerLook>();
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
        _isPickingMagicHash = Animator.StringToHash("pickingMagic");
        _isPickingMagicVariantHash = Animator.StringToHash("pickingMagicVariant");
        _isSendingMagicHash = Animator.StringToHash("sendingMagic");
        _isSendingMagicVariantHash = Animator.StringToHash("sendingMagicVariant");
        _isHidingHandHash = Animator.StringToHash("hidingHand");
        _isDoingPerspectiveHash = Animator.StringToHash("perspective");
    }

    public void ToggleOrb()
    {
        playerInteract.ToggleOrbs();
    }

    public void HideHandMesh()
    {
        PlayerSendMagic?.Invoke();
    }

    public void AllowInteraction()
    {
        //playerInteract.Interacting = false;
    }
    
    public void ShowHand()
    {
        _animator.SetTrigger(_isShowingHandHash);
        //_playerMovement.CanMove = false;
        //_playerHeadBob.CanMove = false;
        //_playerLook.CanLook = false;
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

    public void SetRandomPlayerPickingMagic()
    {
        _random = Random.Range(0, 2);
        Debug.Log(_random == 0
            ? "Hand should play pickingMagic animation"
            : "Hand should play pickingMagicVariant animation");
        _animator.SetBool(_isPickingMagicHash, true);
    }

    public void SetRandomPlayerSendingMagic()
    {
        _random = Random.Range(0, 2);
        Debug.Log(_random == 0
            ? "Hand should play sendingMagic animation"
            : "Hand should play sendingMagicVariant animation");    
        _animator.SetBool(_isSendingMagicHash, true);
    }

    public void SetPlayerIdleMagic()
    {
        _animator.SetBool(_isMagicOnHandHash, true);
    }

    public void SetPlayerPerspective()
    {
        _animator.SetBool(_isDoingPerspectiveHash, true);
    }

    public void SetPlayerEndPerspective()
    {
        _animator.SetBool(_isDoingPerspectiveHash, false);
    }
    
    public void SetPlayerHideHand()
    {
        _animator.SetBool(_isHidingHandHash, true);
    }
}
