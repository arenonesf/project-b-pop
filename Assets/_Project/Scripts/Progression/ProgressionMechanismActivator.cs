using ProjectBPop.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressionMechanismActivator : MonoBehaviour
{
    [SerializeField] private Mechanism mechanism;
    [SerializeField] int minNumberToActivate;
    private bool activated;

    private void OnEnable()
    {
        
        SceneManager.sceneLoaded += CheckActivateMechanism;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= CheckActivateMechanism;
    }

    private void CheckActivateMechanism(Scene scene, LoadSceneMode loadSceneMode)
    {
        int progresionNumber = GameManager.Instance.ProgressionNumber;

        if (progresionNumber >= minNumberToActivate && !mechanism.Solved && !activated)
        {
            Debug.Log("Activating Mechanism " + "progressionNumber:" + minNumberToActivate);
            mechanism.Activate();
        }
    }
}
