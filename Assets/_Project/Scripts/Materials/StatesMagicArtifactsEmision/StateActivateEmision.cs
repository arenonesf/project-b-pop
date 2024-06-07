using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateActivateEmision : IState
{
    private BlackboardChangeEmision _blackboard;

    public StateActivateEmision(BlackboardChangeEmision blackboard)
    {
        _blackboard = blackboard;
    }
    public void OnEnter()
    {
        //Nothing
    }
    public void OnUpdate()
    {
        _blackboard.Intensity = Mathf.Lerp(_blackboard.Intensity, _blackboard.MaxIntensity, Time.deltaTime * _blackboard.ActivateEmisionSpeed);
        foreach (var material in _blackboard.Materials)
        {
            material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
        }
    }
    public void OnExit()
    {
        _blackboard.Intensity = _blackboard.MaxIntensity;

        foreach (var material in _blackboard.Materials)
        {
            material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
        }
    }
}
