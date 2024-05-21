using ProjectBPop.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanismSolvedEmisionSystem : MonoBehaviour
{
    private Mechanism _mechanism;
    private FSM _stateMachine;
    private BlackboardChangeEmision _blackboard;

    private void Awake()
    {
        _mechanism = GetComponent<Mechanism>();
        _blackboard = GetComponent<BlackboardChangeEmision>();

        _stateMachine = new FSM();

        var activateEmision = new StateActivateEmision(_blackboard);
        var deactivateEmision = new StateDeactivateEmision(_blackboard);
        var idleEmisionDeactivated = new StateIdleEmisionDeactivated(_blackboard);
        var idleEmisionActivated = new StateIdleEmisionActivated(_blackboard);

        At(idleEmisionActivated, deactivateEmision, NotSolved());
        At(idleEmisionDeactivated, activateEmision, Solved());
        At(deactivateEmision, activateEmision, Solved());
        At(activateEmision, deactivateEmision, NotSolved());
        At(deactivateEmision, idleEmisionDeactivated, MinValueToIdleDeactivated());
        At(activateEmision, idleEmisionActivated, MinValueToIdleActivated());

        if (_mechanism.Solved)
        {
            _stateMachine.SetState(idleEmisionActivated);
        }
        else
        {
            _stateMachine.SetState(idleEmisionDeactivated);
        }

        void At(IState from, IState to, Func<bool> condition) =>
            _stateMachine.AddTransition((IState)from, (IState)to, condition);

        Func<bool> Solved() => () => _mechanism.Solved;
        Func<bool> NotSolved() => () => !_mechanism.Solved;
        Func<bool> MinValueToIdleActivated() => () => _blackboard.Intensity > _blackboard.MinActivatedIdleEmisionValue;
        Func<bool> MinValueToIdleDeactivated() => () => _blackboard.Intensity < _blackboard.MinDeactivatedIdleEmisionValue;
    }

    private void Update()
    {
        _stateMachine.OnUpdate();
    }
}
