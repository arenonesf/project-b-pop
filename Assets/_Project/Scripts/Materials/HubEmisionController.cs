using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubEmisionController : MonoBehaviour
{
    
    private FSM _stateMachine;
    private BlackboardChangeEmision _blackboardEmision;
    private BlackboardHubEmision _blackboardHub;

    private void Start()
    {
        _blackboardEmision = GetComponent<BlackboardChangeEmision>();
        _blackboardHub = GetComponent<BlackboardHubEmision>();

        _stateMachine = new FSM();

        var emisionHubActivate = new StateHubActivateEmision(_blackboardEmision, _blackboardHub);
        var emisionDeactivate = new StateDeactivateEmision(_blackboardEmision);
        var emisionIdleDeactivated = new StateIdleEmisionDeactivated(_blackboardEmision);
        var emisionIdleActivated = new StateIdleEmisionActivated(_blackboardEmision);

        At(emisionIdleDeactivated, emisionHubActivate, ProgressionAcomplished());
        At(emisionHubActivate, emisionIdleActivated, IdleActivated());
        At(emisionIdleDeactivated, emisionIdleActivated, ProgressionSurpassed()); 

        _stateMachine.SetState(emisionIdleDeactivated);

        void At(IState from, IState to, Func<bool> condition) =>
            _stateMachine.AddTransition((IState)from, (IState)to, condition);

        Func<bool> ProgressionAcomplished() => () =>  _blackboardHub.MinNumberToActivate == GameManager.Instance.ProgressionNumber && !_blackboardHub.Activated;
        Func<bool> ProgressionNotAcomplished() => () => _blackboardHub.MinNumberToActivate < GameManager.Instance.ProgressionNumber;
        Func<bool> IdleActivated() => () => _blackboardHub.Activated;
        Func<bool> ProgressionSurpassed() => () => _blackboardHub.MinNumberToActivate < GameManager.Instance.ProgressionNumber;
    }

    private void Update()
    {
        _stateMachine.OnUpdate();
    }
}
