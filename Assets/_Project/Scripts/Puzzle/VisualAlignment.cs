using System;
using ProjectBPop.Input;
using ProjectBPop.Interfaces;
using ProjectBPop.Magic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VisualAlignment : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private MagicNode magicNode;
    [SerializeField] private Collider alignCollider;
    [SerializeField] private string playerTag;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask alignLayer;
    [SerializeField] private Mechanism perspectiveActivator;
    
    private Camera _playerCamera;
    private PlayerInteract _playerInteract;
    private bool _onTrigger;
    private bool _activated;
    private bool _playedSound;
    private bool _stopSound;
    public bool Aligned;
    public static Action OnVisionCompleted;
    public Action OnAligning;
    public Action OnStopAligning;
    
    private void OnEnable()
    {
        inputReader.PlayerMagicInteractionEvent += ActivateMechanism;
        SceneManager.sceneLoaded += FindPlayer;
    }

    private void OnDisable()
    {
        inputReader.PlayerMagicInteractionEvent -= ActivateMechanism;
        SceneManager.sceneLoaded -= FindPlayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            if (_activated) return;
            _onTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            _onTrigger = false;
            UIManager.Instance.HidePerspectiveIcon();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (_onTrigger && perspectiveActivator.Solved)
        {
            CheckAlignment();
        }           
    }

    private void CheckAlignment()
    {
        var ray = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        if (!Physics.Raycast(ray.origin, ray.direction, out var hit, rayDistance, alignLayer.value)) 
        {
            Aligned = false;
            UIManager.Instance.HidePerspectiveIcon();
            if (!_playedSound)
            {
                //_playedSound = true;
                //_stopSound = false;
                //OnStopAligning?.Invoke();
            }
            return;
        }
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
        if (hit.collider == alignCollider && !_activated)
        {
            Aligned = true;
            UIManager.Instance.DisplayPerspectiveIcon();
            if (!_playedSound)
            {
                _playedSound = true;
                _stopSound = false;
                OnAligning?.Invoke();
                Debug.Log("PlaySound");
            }          
        }
        else
        {
            Aligned = false;
            UIManager.Instance.HidePerspectiveIcon();
            if (!_stopSound)
            {
                _playedSound = false;
                _stopSound = true;
                OnStopAligning?.Invoke();
                Debug.Log("StopSound");
            }          
        }   
    }
    
    private void ActivateMechanism()
    {
        if (!Aligned || _activated) return;
        _activated = true;
        UIManager.Instance.HidePerspectiveIcon();
        magicNode.Interact();
        OnVisionCompleted?.Invoke();
    }

    private bool CheckMagicType()
    {
        var acceptedType = magicNode.Type;
        return _playerInteract.PlayerMagicSourceType == acceptedType;
    }

    private void FindPlayer(Scene scene, LoadSceneMode mode)
    {
        _playerInteract = GameManager.Instance.GetPlayer().GetComponent<PlayerInteract>();
        _playerCamera = GameManager.Instance.GetPlayer().GetComponentInChildren<Camera>();
    }
}
