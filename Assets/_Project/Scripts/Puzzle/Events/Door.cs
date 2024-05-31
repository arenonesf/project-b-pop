using ProjectBPop.Interfaces;
using UnityEngine;

public class Door : Mechanism
{
    [SerializeField] private float speed;
    [SerializeField] private Transform target;
    [SerializeField] private Transform origin;
    public Vector3 CurrentTarget;
    private BlackboardDoor _blackboard;

    private void Awake()
    {
        _blackboard = GetComponent<BlackboardDoor>();
    }

    public override void Activate()
    {
        Solved = true;
        _blackboard.Moving = true;
        CurrentTarget = target.position;
    }

    public override void Deactivate()
    {
        Solved = false;
        _blackboard.Moving = true;
        CurrentTarget = origin.position;
    }
}
