using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ProjectBPop.Player;
using System;
using UnityEditor;
using static UnityEngine.Rendering.DebugUI;

public class PlayerAudioEvents : MonoBehaviour
{
    [SerializeField] private EventReference footstepEvent;
    [SerializeField] private EventReference jumpEvent;
    [SerializeField] private EventReference landEvent;
    [SerializeField] private EventReference giveMagicEvent;
    [SerializeField] private EventReference takeMagicEvent;
    [SerializeField] private float walkFootstepRate;
    [SerializeField] private float runFootstepRate;
    [SerializeField] private LayerMask surfaceLayers;
    [SerializeField][Range(0,1)] private float surfaceFloat;
    [SerializeField] private float _timeToLandSound = 0.1f;
    private float _currentFootstepRate;
    private float time;
    private float landtimer;
    private PlayerMovement _playerMovement;
    private CharacterController _characterController;
    
    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _characterController = GetComponent<CharacterController>();
        _playerMovement.OnJump += PlayJump;
        MagicArtifact.GiveMagic += PlayGiveMagic;
        MagicArtifact.TakeMagic += PlayTakeMagic;
    }

    private void OnDisable()
    {
        _playerMovement.OnJump -= PlayJump;
        MagicArtifact.GiveMagic -= PlayGiveMagic;
        MagicArtifact.TakeMagic -= PlayTakeMagic;
    }

    void Update()
    {
        time += Time.deltaTime;
        CheckSurface();
        CheckOnPlayingFootstep();
        CheckPlayerLanded();
    }

    private void CheckSurface()
    {
        if (!_playerMovement.PlayerIsGrounded) return;

        Ray ray = new Ray(_characterController.transform.position, Vector3.down);
        RaycastHit rayCastHit;

        if (Physics.Raycast(ray, out rayCastHit, 0.15f, surfaceLayers))
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
                case 15:
                    surfaceFloat = 3;
                break;
                case 16:
                    surfaceFloat = 2;
                    break;
                default:
                    surfaceFloat = 0;
                break;
            }

            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Surface", surfaceFloat);
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

    private void CheckPlayerLanded()
    {
        
        if (_playerMovement.PlayerIsGrounded || _characterController.velocity.magnitude < 5 && _playerMovement.PlayerIsGrounded) return;
        
        landtimer += Time.deltaTime;
        
        Ray ray = new Ray(_characterController.transform.position, Vector3.down);
        RaycastHit rayCastHit;

        if (Physics.Raycast(ray, out rayCastHit, 0.1f, surfaceLayers))
        {
            if (landtimer >= _timeToLandSound)
            {
                FMOD.Studio.EventInstance landInstance = RuntimeManager.CreateInstance(landEvent);
                landInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
                landInstance.start();
                landInstance.release();
                landtimer = 0;
            }           
        }

    }


    private void PlayFootstep()
    {
        
        FMOD.Studio.EventInstance footstepInstance = RuntimeManager.CreateInstance(footstepEvent);
        footstepInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        if (!_playerMovement.MovingInputPressed)
        {
            footstepInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            footstepInstance.release();
        }
        footstepInstance.start();
        footstepInstance.release();
    }

    private void PlayJump()
    {
        FMOD.Studio.EventInstance jumpInstance = RuntimeManager.CreateInstance(jumpEvent);
        jumpInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        jumpInstance.start();
        jumpInstance.release();
    }

    private void PlayLand()
    {
        FMOD.Studio.EventInstance landInstance = RuntimeManager.CreateInstance(landEvent);
        landInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        landInstance.start();
        landInstance.release();
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
