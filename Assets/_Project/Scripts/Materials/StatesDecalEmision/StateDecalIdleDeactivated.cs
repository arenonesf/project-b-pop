using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDecalIdleDeactivated : IState
{
    private BlackboardChangeEmision _blackboard;

    public StateDecalIdleDeactivated(BlackboardChangeEmision blackboard)
    {
        _blackboard = blackboard;
    }
    public void OnEnter()
    {
        _blackboard.Intensity = 0f;
        _blackboard.Material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
        _blackboard.Material.color = new Color(_blackboard.Material.color.r, _blackboard.Material.color.g, _blackboard.Material.color.b, 0);
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
