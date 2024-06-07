using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAudio : MonoBehaviour
{
    [SerializeField] private EventReference enterPortalEvent;
    [SerializeField] private EventReference exitPortalEvent;
    private SceneLoaderZone _sceneLoaderZone;

    private void OnEnable()
    {
        _sceneLoaderZone.OnEnterPortal += EnterPortalSound;
        GameManager.OnExitPortal += ExitPortalSound;
    }

    private void OnDisable()
    {
        _sceneLoaderZone.OnEnterPortal -= EnterPortalSound;
        GameManager.OnExitPortal -= ExitPortalSound;
    }

    private void Awake()
    {
        _sceneLoaderZone = GetComponent<SceneLoaderZone>();
    }

    private void EnterPortalSound()
    {
        FMOD.Studio.EventInstance EnterPortalInstance = RuntimeManager.CreateInstance(enterPortalEvent);
        EnterPortalInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        EnterPortalInstance.start();
        EnterPortalInstance.release();
    }

    private void ExitPortalSound()
    {
        FMOD.Studio.EventInstance ExitPortalInstance = RuntimeManager.CreateInstance(exitPortalEvent);
        ExitPortalInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        ExitPortalInstance.start();
        ExitPortalInstance.release();
    }



}
