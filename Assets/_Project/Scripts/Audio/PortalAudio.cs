using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAudio : MonoBehaviour
{
    [SerializeField] private EventReference enterPortalEvent;
    private SceneLoaderZone _sceneLoaderZone;

    private void OnEnable()
    {
        _sceneLoaderZone.OnEnterPortal += EnterPortalSound;
    }

    private void OnDisable()
    {
        _sceneLoaderZone.OnEnterPortal -= EnterPortalSound;
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
}
