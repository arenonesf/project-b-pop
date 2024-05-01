using ProjectBPop.Interfaces;
using UnityEngine;

public class MovingPlatform : Mechanism
{
    [SerializeField] private float speed;
    private bool _shouldOpen;
    [SerializeField] private Transform target;
    [SerializeField] private Transform origin;
    private Vector3 _currentTarget;
    private float _currentSpeed;

    private void Update()
    {
        if (!_shouldOpen) return;
        Move(_currentTarget);
    }

    private void MoveToDesiredTarget()
    {
        _shouldOpen = true;
        _currentTarget = target.position;
        _currentSpeed = speed;
    }

    private void Move(Vector3 newTarget)
    {
        var direction = newTarget - transform.position;
        direction.Normalize();
        transform.Translate(direction * (_currentSpeed * Time.deltaTime));
        if (Vector3.Distance(transform.position, newTarget) <= 0.1f)
        {
            _currentSpeed = 0;
        }
    }
    
    public override void Activate()
    {
        MoveToDesiredTarget();
    }

    public override void Deactivate()
    {
        ReturnToOriginalPosition();
    }

    private void ReturnToOriginalPosition()
    {
        _shouldOpen = true;
        _currentTarget = origin.position;
        _currentSpeed = speed;
    }
}
