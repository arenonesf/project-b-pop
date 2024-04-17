using System;
using System.Collections;
using ProjectBPop.Input;
using UnityEngine;

public class PlayerVault : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private LayerMask vaultingLayer;
    [SerializeField] private float rayDistance;
    private Transform _playerCameraTransform;
    private Rigidbody _playerRigidBody;

    private void Awake()
    {
        _playerCameraTransform = GetComponentInChildren<Camera>().transform;
        _playerRigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        inputReader.PlayerJumpStartedEvent += TryVault;
    }

    private void OnDisable()
    {
        inputReader.PlayerJumpStartedEvent -= TryVault;
    }
    
    private void TryVault()
    {
        if (!Physics.Raycast(_playerCameraTransform.position, _playerCameraTransform.forward, out var hit, rayDistance,
                vaultingLayer.value)) return;
        if (Physics.Raycast(hit.point + (_playerCameraTransform.forward * 0.8f) + (Vector3.up * (0.6f * 2f)), Vector3.down, out var secondHit, 2f))
        {
            StartCoroutine(MovePlayer(secondHit.point, 0.3f));
        }
    }

    private IEnumerator MovePlayer(Vector3 toGo, float duration)
    {
        _playerRigidBody.isKinematic = true;
        var time = 0f;
        var startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, toGo, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = toGo;
        _playerRigidBody.isKinematic = false;
    }
}
