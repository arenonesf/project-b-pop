using System.Collections;
using ProjectBPop.Player;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField] private bool enable = true;
    [SerializeField, Range(0f, 0.1f)] private float amplitude = 0.015f;
    [SerializeField, Range(0f, 30f)] private float frequency = 10f;
    [SerializeField] private float toggleSpeed = 1f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float timeToResetCamera = 0.3f;

    private Transform _camera;
    private Transform _cameraHolder;
    private Vector3 _startPosition;
    private CharacterController _characterController;
    private PlayerMovement _playerMovement;
    private bool _coroutineRunning;

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>().transform;
        _cameraHolder = transform.GetChild(0);
        _characterController = GetComponent<CharacterController>();
        _startPosition = _camera.transform.localPosition;
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(!enable) return;
        CheckMotion();
        ResetPosition();
        _camera.LookAt(FocusTarget());
    }

    private void CheckMotion()
    {
        var speed = new Vector3(_characterController.velocity.x, 0, _characterController.velocity.z).magnitude;
        if(speed < toggleSpeed) return;
        if(!_playerMovement.PlayerIsGrounded) return;
        _camera.localPosition += FootStepMotion();
    }

    private void ResetPosition()
    {
        if(_camera.localPosition == _startPosition) return;
        if (!(_characterController.velocity.magnitude <= 1f)) return;
        Debug.Log("RESETTING CAM");
        if(_coroutineRunning) return;
        StartCoroutine(ResetCamera(_camera.localPosition, _startPosition, timeToResetCamera));
        _coroutineRunning = true;
    }

    private Vector3 FootStepMotion()
    {
        Debug.Log(_playerMovement.PlayerSpeed);
        var position = Vector3.zero;
        position.y += Mathf.Sin(Time.time * frequency) * amplitude;
        if (_playerMovement.PlayerSpeed >= runSpeed)
        {
            position.x += Mathf.Cos(Time.time * frequency / 2) * amplitude * 2;
        }
        
        return position;
    }

    private Vector3 FocusTarget()
    {
        var position = new Vector3(transform.position.x, transform.position.y + _cameraHolder.localPosition.y,
            transform.position.z);
        position += _cameraHolder.forward * 15f;
        return position;
    }

    public void UpdateFrequency(bool isRunning)
    {
        frequency = isRunning ? 21f : 7f;
    }

    private IEnumerator ResetCamera(Vector3 current, Vector3 target, float delay)
    {
        var elapsedTime = 0f;

        while (elapsedTime < delay)
        {
            var time = elapsedTime / delay;
            _camera.localPosition = Vector3.Lerp(current, target, time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _camera.localPosition = target;
        _coroutineRunning = false;
    }

}
