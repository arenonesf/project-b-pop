using System;
using System.Collections;
using System.Collections.Generic;
using ProjectBPop.Interfaces;
using UnityEngine;

public class MovingPlatform : Mechanism
{
    [SerializeField] private List<Transform> targetTransforms;
    [SerializeField] private Transform origin;
    private bool _canMove;
    private int _currentTransformIndex;
    private Rigidbody _rigidbody;
    private IEnumerator _routine;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
           MovePlatform();
        }
    }

    private bool TargetPositionArrived()
    {
        return Vector3.Distance(transform.position, targetTransforms[_currentTransformIndex].position) <= 1.5f;
    }

    private void SetNextTarget()
    {
        if (_currentTransformIndex >= targetTransforms.Count - 1)
        {
            _currentTransformIndex = 0;
        }
        else
        {
            _currentTransformIndex++;
        }
    }

    private void MovePlatform()
    {
        if (!TargetPositionArrived())
        {
            if (_routine == null)
            {
                _routine = Move(targetTransforms[_currentTransformIndex].position, 2f);
                StartCoroutine(_routine);
            }
           
        }
        else
        {
            SetNextTarget();
        }
    }

    private IEnumerator Move(Vector3 toGo, float duration)
    {
        var time = 0f;
        var startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, toGo, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = toGo;
        _routine = null;
    }

    public override void Activate()
    {
        _canMove = true;
    }

    public override void Deactivate()
    {
        _canMove = false;
        Solved = false;
    }
}
