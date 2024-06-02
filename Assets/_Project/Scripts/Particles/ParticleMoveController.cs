using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMoveController : MonoBehaviour
{
    private FSM _stateMachine;
    private BlackboardParticleMove _blackboard;

    private void Awake()
    {
        _blackboard = GetComponent<BlackboardParticleMove>();

        _stateMachine = new FSM();

        var moving = new StateParticleMoveMoving(_blackboard);
        var idle = new StateParticleMoveIdle(_blackboard);

        At(idle, moving, Move());
        At(moving, idle, StopMoving());

        _stateMachine.SetState(idle);

        void At(IState from, IState to, Func<bool> condition) =>
            _stateMachine.AddTransition((IState)from, (IState)to, condition);

        Func<bool> Move() => () => _blackboard.Moving;
        Func<bool> StopMoving() => () => Vector3.Distance(_blackboard.CurrentTarget, _blackboard.MagicParticles.transform.position) <= _blackboard.DiferenceToChangeState;
    }

    private void Update()
    {
        _stateMachine.OnUpdate();
    }
}
