using ProjectBPop.Interfaces;
using ProjectBPop.Player;
using UnityEngine;

public class MovingPlatformLoop : Mechanism
{
    [SerializeField] private PlatformPath waypointPath;
    [SerializeField] private float speed;

    private int _targetWaypointIndex;
    private Transform _previousWaypoint;
    private Transform _targetWaypoint;
    private float _timeToWaypoint;
    private float _elapsedTime;

    private void Start()
    {
        TargetNextWaypoint();
    }

    private void FixedUpdate()
    {
        if (!Solved) return;
        _elapsedTime += Time.deltaTime;

        var elapsedPercentage = _elapsedTime / _timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage);
        transform.rotation = Quaternion.Lerp(_previousWaypoint.rotation, _targetWaypoint.rotation, elapsedPercentage);

        if (elapsedPercentage >= 1)
        {
            TargetNextWaypoint();
        }
    }

    private void TargetNextWaypoint()
    {
        _previousWaypoint = waypointPath.GetWaypoint(_targetWaypointIndex);
        _targetWaypointIndex = waypointPath.GetNextWaypointIndex(_targetWaypointIndex);
        _targetWaypoint = waypointPath.GetWaypoint(_targetWaypointIndex);

        _elapsedTime = 0;

        var distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        _timeToWaypoint = distanceToWaypoint / speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player")) return;
        other.transform.TryGetComponent(out PlayerMovement playerReference);
        other.transform.SetParent(playerReference.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.transform.CompareTag("Player")) return;
        other.transform.TryGetComponent(out PlayerMovement playerReference);
        other.transform.SetParent(null);
    }
    
    public override void Activate()
    {
        Solved = true;
    }

    public override void Deactivate()
    {
        Solved = false;
    }
}
