using FMODUnity;
using ProjectBPop.Interfaces;
using UnityEngine;

public class MovingPlatform : Mechanism
{
    [SerializeField] private float speed;
    private bool _shouldOpen;
    [SerializeField] private Transform target;
    [SerializeField] private Transform origin;
    [SerializeField] private EventReference movingPlatformEvent;
    private Vector3 _currentTarget;
    private float _currentSpeed;
    private FMOD.Studio.EventInstance movingPlatformInstance;

    private void Awake()
    {
        movingPlatformInstance = RuntimeManager.CreateInstance(movingPlatformEvent);
        movingPlatformInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
    }

    private void OnDisable()
    {
        movingPlatformInstance.release();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

    private void FixedUpdate()
    {
        if (!_shouldOpen) return;
        Move(_currentTarget);
    }

    private void MoveToDesiredTarget()
    {
        Solved = true;
        _shouldOpen = true;
        _currentTarget = target.position;
        _currentSpeed = speed;
    }

    private void Move(Vector3 newTarget)
    {
        var direction = newTarget - transform.position;
        direction.Normalize();
        transform.Translate(direction * (_currentSpeed * Time.fixedDeltaTime));
        if (Vector3.Distance(transform.position, newTarget) <= 0.1f)
        {
            _currentSpeed = 0;
            movingPlatformInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }   
    }

    public override void Activate()
    {
        MoveToDesiredTarget();      
        movingPlatformInstance.start();
    }

    public override void Deactivate()
    {
        ReturnToOriginalPosition();
        movingPlatformInstance.start();
    }

    private void ReturnToOriginalPosition()
    {
        Solved = false;
        _shouldOpen = true;
        _currentTarget = origin.position;
        _currentSpeed = speed;
    }
}
