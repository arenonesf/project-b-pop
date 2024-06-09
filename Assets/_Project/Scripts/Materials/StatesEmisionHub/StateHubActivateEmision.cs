using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHubActivateEmision : IState
{
    private BlackboardHubEmision _blackboardHub;
    private BlackboardChangeEmision _blackboardEmision;
    private FMOD.Studio.EventInstance hubProgressionEvent;

    public StateHubActivateEmision(BlackboardChangeEmision blackboardEmision, BlackboardHubEmision blackboardHub)
    {
        _blackboardEmision = blackboardEmision;
        _blackboardHub = blackboardHub;
    }
    public void OnEnter()
    {
        Debug.Log("ActivateEmisionHub");
        switch (GameManager.Instance.ProgressionNumber)
        {
            case 1:
                hubProgressionEvent = RuntimeManager.CreateInstance(_blackboardHub.Hub1SoundEvent);
            break;
            case 2:
                hubProgressionEvent = RuntimeManager.CreateInstance(_blackboardHub.Hub2SoundEvent);
                break;
            case 3:
                hubProgressionEvent = RuntimeManager.CreateInstance(_blackboardHub.Hub3SoundEvent);
            break;
            default:
            break;
        }
        foreach (GameObject particle in _blackboardHub.Particles) particle.SetActive(true);

        hubProgressionEvent.set3DAttributes(RuntimeUtils.To3DAttributes(_blackboardHub.gameObject));
        hubProgressionEvent.start();
        hubProgressionEvent.release();
    }
    public void OnUpdate()
    {
        _blackboardEmision.Intensity = Mathf.Lerp(_blackboardEmision.Intensity, _blackboardEmision.MaxIntensity, Time.deltaTime * _blackboardEmision.ActivateEmisionSpeed);

        foreach (var material in _blackboardEmision.Materials)
        {
            material.SetVector("_EmissionColor", _blackboardEmision.EmissionColorValue * _blackboardEmision.Intensity);
        }


        if (_blackboardEmision.Intensity > _blackboardEmision.MinActivatedIdleEmisionValue)
        {
            _blackboardHub.Activated = true;
        }
    }
    public void OnExit()
    {
        _blackboardEmision.Intensity = _blackboardEmision.MaxIntensity;

        foreach (var material in _blackboardEmision.Materials)
        {
            material.SetVector("_EmissionColor", _blackboardEmision.EmissionColorValue * _blackboardEmision.Intensity);
        }
    }
}
