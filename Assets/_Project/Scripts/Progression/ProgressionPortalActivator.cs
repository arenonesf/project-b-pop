using ProjectBPop.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressionPortalActivator : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    [SerializeField] private int minNumberToDeactivate;

    private void OnEnable()
    {

        SceneManager.sceneLoaded += CheckActivatePortal;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= CheckActivatePortal;
    }

    private void CheckActivatePortal(Scene scene, LoadSceneMode loadSceneMode)
    {
        int progresionNumber = GameManager.Instance.ProgressionNumber;

        if (progresionNumber < minNumberToDeactivate)
        {
            portal.SetActive(true);
        }
        if (progresionNumber >= minNumberToDeactivate)
        {
            portal.SetActive(false);
        }
    }
}
