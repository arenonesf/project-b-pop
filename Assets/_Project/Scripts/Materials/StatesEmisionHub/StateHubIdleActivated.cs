using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHubIdleActivated : IState
{
    private BlackboardChangeEmision _blackboardEmision;
    private BlackboardHubEmision _blackboardHub;

    public StateHubIdleActivated(BlackboardChangeEmision blackboardEmision, BlackboardHubEmision blackboardHub)
    {
        _blackboardEmision = blackboardEmision;
        _blackboardHub = blackboardHub;
    }

    public void OnEnter()
    {
        _blackboardEmision.Intensity = _blackboardEmision.MaxIntensity;
        foreach (var material in _blackboardEmision.Materials)
        {
            material.SetVector("_EmissionColor", _blackboardEmision.EmissionColorValue * _blackboardEmision.Intensity);
        }

        foreach (GameObject particle in _blackboardHub.Particles) particle.SetActive(true);
    }

    public void OnExit()
    {
        //Nothing
    }

    public void OnUpdate()
    {
        //Nothing
    }
}
