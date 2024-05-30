using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDoorMoving : IState
{
    private BlackboardDoor _blackboard;

    public StateDoorMoving(BlackboardDoor blackboard)
    {
        _blackboard = blackboard;
    }

    public void OnEnter()
    {
        //Nothing
    }

    public void OnUpdate()
    {
        if (_blackboard.Door.CurrentTarget == Vector3.zero) return;
        _blackboard.Door.transform.position = Vector3.MoveTowards(_blackboard.Door.transform.position, _blackboard.Door.CurrentTarget, _blackboard.Speed * Time.deltaTime);
    }

    public void OnExit()
    {
        _blackboard.Moving = false;

        if (_blackboard.Door.CurrentTarget == _blackboard.OpenPosition.position)
        {
            _blackboard.Door.transform.position = _blackboard.OpenPosition.position;
        }

        if (_blackboard.Door.CurrentTarget == _blackboard.ClosePosition.position)
        {
            _blackboard.Door.transform.position = _blackboard.ClosePosition.position;
        }


    }
}
