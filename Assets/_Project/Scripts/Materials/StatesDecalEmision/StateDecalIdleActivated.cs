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
        _blackboard.Material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
        _blackboard.Material.color = new Color(_blackboard.Material.color.r, _blackboard.Material.color.g, _blackboard.Material.color.b, 1);
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
