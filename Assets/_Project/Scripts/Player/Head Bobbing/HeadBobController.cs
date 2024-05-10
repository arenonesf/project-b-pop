using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField] private bool enable = true;

    [SerializeField, Range(0f, 0.1f)] private float amplitude = 0.015f;

    [SerializeField, Range(0f, 30f)] private float frequency = 10f;

    [SerializeField] private float toggleSpeed = 1f;

    private Transform _camera;
    private Transform _cameraHolder;
    private Vector3 _startPosition;
    private CharacterController _characterController;

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>().transform;
        _cameraHolder = transform.GetChild(0);
        _characterController = GetComponent<CharacterController>();
        _startPosition = _camera.transform.localPosition;
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
        ResetPosition();
        if(speed < toggleSpeed) return;
        if(!_characterController.isGrounded) return;
        _camera.localPosition += FootStepMotion();
    }

    private void ResetPosition()
    {
        if(_camera.localPosition == _startPosition) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPosition, 1 * Time.deltaTime);
    }

    private Vector3 FootStepMotion()
    {
        var position = Vector3.zero;
        position.y += Mathf.Sin(Time.time * frequency) * amplitude;
        //position.x += Mathf.Cos(Time.time * frequency / 2) * amplitude * 2;
        return position;
    }

    private Vector3 FocusTarget()
    {
        var position = new Vector3(transform.position.x, transform.position.y + _cameraHolder.localPosition.y,
            transform.position.z);
        position += _cameraHolder.forward * 15f;
        return position;
    }
}
