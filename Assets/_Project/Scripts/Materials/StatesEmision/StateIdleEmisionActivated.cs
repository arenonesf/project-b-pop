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
        Debug.Log("Activated Idle");
        _blackboard.Intensity = 10f;
        _blackboard.Material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
    }

    public void OnExit()
    {
        
    }

    public void OnUpdate()
    {
        
    }
}
