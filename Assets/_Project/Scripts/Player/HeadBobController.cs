using System;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField] private bool enable = true;
    [SerializeField, Range(0f, 0.1f)] private float amplitudeY = 0.0146f;
    [SerializeField, Range(0f, 0.1f)] private float amplitudeX = 0.0008f;
    [SerializeField, Range(0f, 30f)] private float frequency = 10f;
    [SerializeField] private Transform cameraHolder;
    private Transform _camera;

    private float _toggleSpeed = 3f;
    private Vector3 _startPosition;
    private CharacterController _characterController;

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>().transform;
        _characterController = GetComponent<CharacterController>();
        _startPosition = _camera.localPosition;
    }

    private void Update()
    {
        if (!enable) return;
        CheckMotion();
        ResetPosition();
    }

    private void CheckMotion()
    {
        var controllerVelocity = _characterController.velocity;
        var speed = new Vector3(controllerVelocity.x, 0f, controllerVelocity.z).magnitude;
        
        ResetPosition();
        if(speed < _toggleSpeed) return;
        if(!_characterController.isGrounded) return;

        PlayMotion(FootStepMotion());
    }

    private void PlayMotion(Vector3 motion)
    {
        _camera.localPosition += motion;
    }

    public void UpdateFrequency(bool isRunning)
    {
        frequency = isRunning ? 20f : 7f;
        amplitudeX = isRunning ? 0.0068f : 0.0008f;
    }

    private Vector3 FootStepMotion()
    {
        var position = Vector3.zero;
        position.y += Mathf.Sin(Time.time * frequency) * amplitudeY;
        position.x += Mathf.Cos(Time.time * frequency / 2f) * amplitudeX * 2f;
        return position;
    }

    private void ResetPosition()
    {
        if (_camera.localPosition == _startPosition) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPosition, 1 * Time.deltaTime);
    }
}
