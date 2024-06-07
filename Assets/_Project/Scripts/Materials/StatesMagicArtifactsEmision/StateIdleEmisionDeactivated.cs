using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateIdleEmisionDeactivated : IState
{
    private BlackboardChangeEmision _blackboard;

    public StateIdleEmisionDeactivated(BlackboardChangeEmision blackboard)
    {
        _blackboard = blackboard;
    }
    public void OnEnter()
    {
        _blackboard.Intensity = _blackboard.MinIntensity;

        foreach (var material in _blackboard.Materials)
        {
            material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
        }
    }
    public void OnUpdate()
    {
        //Nothing
    }
    public void OnExit()
    {
        //Nothing
    }
}
