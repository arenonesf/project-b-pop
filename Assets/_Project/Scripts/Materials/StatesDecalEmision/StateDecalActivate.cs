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
        _blackboard.Material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
        _blackboard.Material.color = new Color(_blackboard.Material.color.r, _blackboard.Material.color.g, _blackboard.Material.color.b, Mathf.Lerp(_blackboard.Material.color.a, 1, Time.deltaTime * _blackboard.ActivateOpacitySpeed));
    }
    public void OnExit()
    {
        _blackboard.Intensity = _blackboard.MaxIntensity;
        _blackboard.Material.SetVector("_EmissionColor", _blackboard.EmissionColorValue * _blackboard.Intensity);
        _blackboard.Material.color = new Color(_blackboard.Material.color.r, _blackboard.Material.color.g, _blackboard.Material.color.b, 1);
    }
}
