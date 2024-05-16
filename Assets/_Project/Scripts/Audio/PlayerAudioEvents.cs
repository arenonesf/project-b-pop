using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ProjectBPop.Player;

public class PlayerAudioEvents : MonoBehaviour
{
    [SerializeField] private EventReference footstepEvent;
    [SerializeField] private float walkFootstepRate;
    [SerializeField] private float runFootstepRate;
    private float _currentFootstepRate;
    [SerializeField][Range(0,1)] private float surfaceFloat;
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
        CheckOnPlayingFootstep();
    }

    private void CheckOnPlayingFootstep()
    {
        if (_characterController.velocity.magnitude < 0.5)
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
        if (_characterController.velocity.magnitude < 0.5)
        {
            Debug.Log("Parando");
            footstepInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            footstepInstance.release();
        }
        footstepInstance.start();
        footstepInstance.release();



    }
}
