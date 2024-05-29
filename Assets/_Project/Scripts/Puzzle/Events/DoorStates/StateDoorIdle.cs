using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDoorIdle : IState
{
    private BlackboardDoor _blackboard;

    public StateDoorIdle(BlackboardDoor blackboard)
    {
        _blackboard = blackboard;
    }

    public void OnEnter()
    {
        if (_blackboard.Door.Solved)
        {
            _blackboard.Door.transform.position = _blackboard.OpenPosition.position;
        }
        else
        {
            _blackboard.Door.transform.position = _blackboard.ClosePosition.position;
        }
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
