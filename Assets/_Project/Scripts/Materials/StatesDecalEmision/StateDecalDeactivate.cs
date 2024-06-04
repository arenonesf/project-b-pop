using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDecalDeactivate : IState
{
    private BlackboardChangeEmision _blackboard;

    public StateDecalDeactivate(BlackboardChangeEmision blackboard)
    {
        _blackboard = blackboard;
    }
    public void OnEnter()
    {
        //Nothing
    }
    public void OnUpdate()
    {
        
        _blackboard.Intensity = Mathf.Lerp(_blackboard.Intensity, 0, Time.deltaTime * _blackboard.DeactivateEmisionSpeed);
        _blackboard.Alpha = Mathf.Lerp(_blackboard.Alpha, 0, Time.deltaTime * _blackboard.ActivateOpacitySpeed);

        foreach (var material in _blackboard.Materials)
        {
            material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
            material.color = new Color(material.color.r, material.color.g, material.color.b, _blackboard.Alpha);
        }
    }
    public void OnExit()
    {
        _blackboard.Intensity = 0f;
        _blackboard.Alpha = 0;
        foreach (var material in _blackboard.Materials)
        {
            material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
            material.color = new Color(material.color.r, material.color.g, material.color.b, _blackboard.Alpha);
        }
    }
}
