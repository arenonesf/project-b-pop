using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdleEmisionActivated : IState
{
    private BlackboardChangeEmision _blackboard;

    public StateIdleEmisionActivated(BlackboardChangeEmision blackboard)
    {
        _blackboard = blackboard;
    }

    public void OnEnter()
    {
        Debug.Log("IDLE ACTIVATED" + _blackboard.gameObject.name);
        _blackboard.Intensity = _blackboard.MaxIntensity;
        foreach (var material in _blackboard.Materials)
        {
            material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
        }
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
