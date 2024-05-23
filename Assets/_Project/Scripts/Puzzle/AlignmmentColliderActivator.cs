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
            ActivateCollider();
        }          
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("AlignmentCollider"))
        {
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
