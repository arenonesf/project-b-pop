using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
        _blackboard.Intensity = 0f;
        _blackboard.Material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
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
