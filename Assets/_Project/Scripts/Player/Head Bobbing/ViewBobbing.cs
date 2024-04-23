using ProjectBPop.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class ViewBobbing : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float amplitudeIntensity;
    [SerializeField] private float effectSpeed;
    private Vector3 _originalOffset;
    private Vector3 _currentOffset;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
    private float _sinTime;
    private PlayerMovement _playerMovement;

    void Start()
    {
        _playerMovement = GetComponentInParent<PlayerMovement>();
        _originalOffset = transform.localPosition;
    }

    void Update()
    {
        ViewBobbingMovementUpdate();
    }

    private void ViewBobbingMovementUpdate()
    {
        if (_playerMovement.PlayerVelocity.magnitude > 0)
        {
            _sinTime += Time.deltaTime * effectSpeed;
        }
        else
        {
            
            _sinTime = Mathf.SmoothStep(_sinTime, _originalOffset.y, Time.deltaTime); 
        }

        float amplitude = -Mathf.Abs(amplitudeIntensity * Mathf.Sin(_sinTime));

        _currentOffset = new Vector3
        {
            x = 0,
            y = _originalOffset.y + amplitude,
            z = 0
        };

        TargetPositionUpdate();
    }

    private void TargetPositionUpdate()
    {
        target.localPosition = new Vector3(target.localPosition.x, _currentOffset.y, target.localPosition.z);
    }
}
