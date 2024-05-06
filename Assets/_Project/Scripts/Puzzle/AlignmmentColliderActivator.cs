using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlignmmentColliderActivator : MonoBehaviour
{
    [SerializeField] private Collider alignmentCollider;

    private void Start()
    {
        DeactivateCollider();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AlignmentCollider"))
        {
            Debug.Log("Enter");
            ActivateCollider();
        }          
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("AlignmentCollider"))
        {
            Debug.Log("Exit");
            DeactivateCollider();
        }
    }
    private void ActivateCollider()
    {
        alignmentCollider.enabled = true;
    }

    private void DeactivateCollider()
    {
        alignmentCollider.enabled = false;
    }

}
