using ProjectBPop.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanismSolvedEmisionSystem : MonoBehaviour
{
    [SerializeField] private Mechanism mechanism;
    private FSM _stateMachine;
    private BlackboardChangeEmision _blackboard;

    private void Awake()
    {
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

        if (mechanism.Solved)
        {
            _stateMachine.SetState(idleEmisionActivated);
        }
        else
        {
            _stateMachine.SetState(idleEmisionDeactivated);
        }

        void At(IState from, IState to, Func<bool> condition) =>
            _stateMachine.AddTransition((IState)from, (IState)to, condition);

        Func<bool> Solved() => () => mechanism.Solved;
        Func<bool> NotSolved() => () => !mechanism.Solved;
        Func<bool> MinValueToIdleActivated() => () => _blackboard.Intensity > _blackboard.MinActivatedIdleEmisionValue;
        Func<bool> MinValueToIdleDeactivated() => () => _blackboard.Intensity < _blackboard.MinDeactivatedIdleEmisionValue;
    }

    private void Update()
    {
        _stateMachine.OnUpdate();
    }
}
