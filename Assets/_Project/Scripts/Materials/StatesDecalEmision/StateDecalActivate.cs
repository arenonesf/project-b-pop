using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDecalActivate : IState
{
    private BlackboardChangeEmision _blackboard;

    public StateDecalActivate(BlackboardChangeEmision blackboard)
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
        _blackboard.Alpha = Mathf.Lerp(_blackboard.Alpha, 1, Time.deltaTime * _blackboard.ActivateOpacitySpeed);

        foreach (var material in _blackboard.Materials)
        {
            material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
            material.color = new Color(material.color.r, material.color.g, material.color.b, _blackboard.Alpha);
        }
    }
    public void OnExit()
    {
        _blackboard.Intensity = _blackboard.MaxIntensity;
        _blackboard.Alpha = 1;
        foreach (var material in _blackboard.Materials)
        {
            material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
            material.color = new Color(material.color.r, material.color.g, material.color.b, _blackboard.Alpha);
        }
    }
}
