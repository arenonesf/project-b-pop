using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ProjectBPop.Player;
using System;
using UnityEditor;

public class PlayerAudioEvents : MonoBehaviour
{
    [SerializeField] private EventReference footstepEvent;
    [SerializeField] private EventReference giveMagicEvent;
    [SerializeField] private EventReference takeMagicEvent;
    [SerializeField] private float walkFootstepRate;
    [SerializeField] private float runFootstepRate;
    [SerializeField] private LayerMask surfaceLayers;
    [SerializeField][Range(0,1)] private float surfaceFloat;
    private float _currentFootstepRate;
    private PlayerMovement _playerMovement;
    private CharacterController _characterController;
    private float time;
    
    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        time += Time.deltaTime;
        CheckSurface();
        CheckOnPlayingFootstep();        
    }

    private void CheckSurface()
    {
        if (!_playerMovement.PlayerIsGrounded) return;

        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit rayCastHit;

        if (Physics.Raycast(ray, out rayCastHit, 0.4f, surfaceLayers))
        {
            int layerIndex = rayCastHit.transform.gameObject.layer;
            switch (layerIndex)
            {
                case 6:
                    surfaceFloat = 0;
                    break;
                case 13:
                    surfaceFloat = 1;
                break;
                default:
                    surfaceFloat = 0;
                break;
            }
        }
    }

    private void CheckOnPlayingFootstep()
    {
        if (_characterController.velocity.magnitude < 0.5 || !_playerMovement.PlayerIsGrounded || _playerMovement.transform.parent != null && !_playerMovement.MovingInputPressed)
        {
            return;
        }
             

        if (_playerMovement.PlayerIsRunning)
        {
            _currentFootstepRate = runFootstepRate;
        }
        else
        {
            _currentFootstepRate = walkFootstepRate;
        }
        
        if (time > _currentFootstepRate)
        {
            time = 0;
            PlayFootstep();       
        }
    }

    private void PlayFootstep()
    {
        
        FMOD.Studio.EventInstance footstepInstance = RuntimeManager.CreateInstance(footstepEvent);
        footstepInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        footstepInstance.setParameterByName("Surface", surfaceFloat);
        if (!_playerMovement.MovingInputPressed)
        {
            footstepInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            footstepInstance.release();
        }
        footstepInstance.start();
        footstepInstance.release();
    }

    private void PlayGiveMagic()
    {
        FMOD.Studio.EventInstance giveMagicInstance = RuntimeManager.CreateInstance(giveMagicEvent);
        giveMagicInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        giveMagicInstance.start();
        giveMagicInstance.release();
    }

    private void PlayTakeMagic()
    {
        FMOD.Studio.EventInstance takeMagicInstance = RuntimeManager.CreateInstance(takeMagicEvent);
        takeMagicInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        takeMagicInstance.start();
        takeMagicInstance.release();
    }


}
