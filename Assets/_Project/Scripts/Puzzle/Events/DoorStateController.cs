using ProjectBPop.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStateController : MonoBehaviour
{
    private Door _door;
    private FSM _stateMachine;
    private BlackboardDoor _blackboard;

    private void Awake()
    {
        _door = GetComponent<Door>();
        _blackboard = GetComponent<BlackboardDoor>();
        _blackboard.Door = _door;

        _stateMachine = new FSM();

        var moving = new StateDoorMoving(_blackboard);
        var idle = new StateDoorIdle(_blackboard);
        var progresionActivated = new StateDoorProgressionActivated(_blackboard);

        At(idle, moving, Move());
        At(moving, idle, StopMoving());
        At(idle, progresionActivated, ProgresionActivated());

        _stateMachine.SetState(idle);

        void At(IState from, IState to, Func<bool> condition) =>
            _stateMachine.AddTransition((IState)from, (IState)to, condition);

        Func<bool> Move() => () => _blackboard.Moving;
        Func<bool> StopMoving() => () => Vector3.Distance(_blackboard.Door.CurrentTarget, _blackboard.Door.transform.position) <= _blackboard.DiferenceToChangeState;
        Func<bool> ProgresionActivated() => () => _blackboard.ProgressionDoor && GameManager.Instance.ProgressionNumber > _blackboard.MinNumberToActivate;
    }

    private void Update()
    {
        _stateMachine.OnUpdate();
    }
}
