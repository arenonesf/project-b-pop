using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDecalIdleActivated : IState
{
    private BlackboardChangeEmision _blackboard;

    public StateDecalIdleActivated(BlackboardChangeEmision blackboard)
    {
        _blackboard = blackboard;
    }
    public void OnEnter()
    {
        _blackboard.Intensity = _blackboard.MaxIntensity;
        _blackboard.Alpha = 1;
        foreach (var material in _blackboard.Materials)
        {
            material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
            material.color = new Color(material.color.r, material.color.g, material.color.b, _blackboard.Alpha);
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
