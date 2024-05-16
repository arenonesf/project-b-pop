using System;
using UnityEngine;

public class UITriggerInteract : MonoBehaviour
{
    [SerializeField] private GameObject interactorUI;

    public Action<GameObject> OnShowingInteraction;
    public Action<GameObject> OnHidingInteraction;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnShowingInteraction?.Invoke(interactorUI);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnHidingInteraction?.Invoke(interactorUI);
        }
    }

}
