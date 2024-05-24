using ProjectBPop.Interfaces;
using UnityEngine;

public class Door : Mechanism
{
    [SerializeField] private float speed;
    [SerializeField] private Transform target;
    [SerializeField] private Transform origin;
    private Vector3 _currentTarget;
    
    private void Update()
    {
        if(_currentTarget == Vector3.zero) return;
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
    }
    
    public override void Activate()
    {
        Solved = true;
        _currentTarget = target.position;
    }

    public override void Deactivate()
    {
        Solved = false;
        _currentTarget = origin.position;
    }
}
