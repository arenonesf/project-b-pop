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
        _blackboard.Intensity = 10f;
        _blackboard.Material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
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
