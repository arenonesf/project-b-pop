using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualAlignment : MonoBehaviour
{
    [SerializeField] private List<GameObject> alignableObjects;
    [SerializeField] private Collider trigger;
    [SerializeField] private string playerTag;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask alignableLayer;
    private bool _onTrigger;

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
    }

    private void CheckAlignment()
    {
        var ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        RaycastHit[] results = Physics.RaycastAll(ray.origin, ray.direction, alignableLayer);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
        if (results.Length == alignableObjects.Count)
            Debug.Log("Alligned");
        else
            Debug.Log("NotAlligned");
    }
}
