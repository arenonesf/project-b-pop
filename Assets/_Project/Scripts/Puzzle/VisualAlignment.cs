using ProjectBPop.Input;
using ProjectBPop.Magic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VisualAlignment : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private MagicNode _magicNode;
    [SerializeField] private Collider alignCollider;
    [SerializeField] private string playerTag;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask alignableLayer;
    private Camera _playerCamera;
    private PlayerInteract _playerInteract;
    private bool _onTrigger;
    private bool _activated;
    public bool Aligned;
    
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
            _onTrigger = true;
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            _onTrigger = false;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if (_onTrigger)
        {
            CheckAlignment();
        }           
    }

    private void CheckAlignment()
    {
        var ray = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        if (!Physics.Raycast(ray.origin, ray.direction, out var hit, rayDistance, alignableLayer.value)) 
        {
            Aligned = false;
            return;
        }
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
        if (hit.collider == alignCollider)
        {
            Aligned = true;
        }
        else
        {
            Aligned = false;
        }
    }
    
    private void ActivateMechanism()
    {
        if (Aligned && !_activated && CheckMagicType())
        {
            _activated = true;
            _magicNode.Interact();
        }
    }

    private bool CheckMagicType()
    {
        var acceptedType = _magicNode.Type;
        return _playerInteract.PlayerMagicSourceType == acceptedType;
    }

    private void FindPlayer(Scene scene, LoadSceneMode mode)
    {
        _playerInteract = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>();
        _playerCamera = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>();
    }
            
}
