using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDoorProgressionActivated : IState
{
    private BlackboardDoor _blackboard;

    public StateDoorProgressionActivated(BlackboardDoor blackboard)
    {
        _blackboard = blackboard;
    }

    public void OnEnter()
    {
        _blackboard.Door.transform.position = _blackboard.OpenPosition.position;
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
