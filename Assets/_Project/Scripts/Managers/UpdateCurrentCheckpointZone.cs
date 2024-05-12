using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCurrentCheckpointZone : MonoBehaviour
{
    [SerializeField] private Transform checkpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CheckpointManager.Instance.ChangeCurrentCheckpoint(checkpoint);
        }
    }
}
