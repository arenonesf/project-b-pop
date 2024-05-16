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
        Debug.Log("Activating");
    }
    public void OnUpdate()
    {
        _blackboard.Intensity = Mathf.Lerp(_blackboard.Intensity, 10, Time.deltaTime * _blackboard.ActivateSpeed);
        _blackboard.Material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
    }
    public void OnExit()
    {
        _blackboard.Intensity = 10f;
        _blackboard.Material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
    }
}
