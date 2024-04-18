using System;
using System.Collections;
using ProjectBPop.Interfaces;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IReact
{
    [SerializeField] private Transform target;
    [SerializeField] private bool _loop;
    private Vector3 _origin;

    private Rigidbody _rigidBody;
    private bool _canMove;

    private void Awake()
    {
        _rigidBody = GetComponentInChildren<Rigidbody>();
        _rigidBody.isKinematic = true;
        _origin = transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

    public void React(bool update)
    {
        StartCoroutine(update ? Move(target.position, 5f) : Move(_origin, 5f));
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
    }
}
