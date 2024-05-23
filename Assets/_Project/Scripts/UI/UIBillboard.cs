using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBillboard : MonoBehaviour
{
    private Transform _playerCameraTransform;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += FindPlayer;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= FindPlayer;
    }


    private void OnDestroy()
    {
        _playerCameraTransform = null;
    }

    private void LateUpdate()
    {
        transform.forward = _playerCameraTransform.forward;
    }

    private void FindPlayer(Scene scene, LoadSceneMode mode)
    {
        _playerCameraTransform = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>().transform;
    }
}
