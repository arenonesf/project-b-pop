using ProjectBPop.Magic;
using UnityEngine;

public class VisualAlignment : MonoBehaviour
{
    [SerializeField] private MagicNode _magicNode;
    [SerializeField] private Collider alignCollider;
    [SerializeField] private Collider trigger;
    [SerializeField] private string playerTag;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask alignableLayer;
    private bool _onTrigger;
    private bool _activated;
    public bool Aligned;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
            _onTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag(playerTag))
            _onTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_onTrigger)
            CheckAlignment();

        if (Aligned && !_activated)
        {
            _activated = true;
            _magicNode.Interact();
        }
    }

    private void CheckAlignment()
    {
        var ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
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
            
}
