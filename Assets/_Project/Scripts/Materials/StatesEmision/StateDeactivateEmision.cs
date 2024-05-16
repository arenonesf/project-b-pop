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
        Debug.Log("Deactivating");
    }
    public void OnUpdate()
    {
        _blackboard.Intensity = Mathf.Lerp(_blackboard.Intensity, 0, Time.deltaTime * _blackboard.DeactivateSpeed);
        _blackboard.Material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
    }
    public void OnExit()
    {
        _blackboard.Intensity = 0f;
        _blackboard.Material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
    }
}
