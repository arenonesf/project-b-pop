using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeProgressionZone : MonoBehaviour
{   
    [SerializeField] private int progressionNumber;
    private bool _activated;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_activated)
        {
            GameManager.Instance.UpdateProgressionNumber(progressionNumber);
            _activated = true;
            Debug.Log("ChangeProgressionNumber");
        }
    }
}
