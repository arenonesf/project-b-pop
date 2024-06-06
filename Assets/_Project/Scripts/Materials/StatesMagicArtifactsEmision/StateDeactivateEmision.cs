using ProjectBPop.Magic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDeactivateEmision : IState
{
    private BlackboardChangeEmision _blackboard;

    public StateDeactivateEmision(BlackboardChangeEmision blackboard)
    {
        _blackboard = blackboard;
    }
    public void OnEnter()
    {
        Debug.Log("STATE DEACTIVATE" + _blackboard.gameObject.name);
        //Nothing
    }
    public void OnUpdate()
    {
        _blackboard.Intensity = Mathf.Lerp(_blackboard.Intensity, _blackboard.MinIntensity, Time.deltaTime * _blackboard.DeactivateEmisionSpeed);
        foreach (var material in _blackboard.Materials)
        {
            material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
        }
    }
    public void OnExit()
    {
        _blackboard.Intensity = _blackboard.MinIntensity;
        foreach (var material in _blackboard.Materials)
        {
            material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
        }
    }
}
