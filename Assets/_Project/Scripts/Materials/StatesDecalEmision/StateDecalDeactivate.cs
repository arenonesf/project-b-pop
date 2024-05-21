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
        _blackboard.Material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
        _blackboard.Material.color = new Color(_blackboard.Material.color.r, _blackboard.Material.color.g, _blackboard.Material.color.b, Mathf.Lerp(_blackboard.Material.color.a, 0, Time.deltaTime * _blackboard.DeactivateOpacitySpeed));
    }
    public void OnExit()
    {
        _blackboard.Intensity = 0f;
        _blackboard.Material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
        _blackboard.Material.color = new Color(_blackboard.Material.color.r, _blackboard.Material.color.g, _blackboard.Material.color.b, 0);
    }
}
