using ProjectBPop.Input;
using UnityEngine;

namespace ProjectBPop.Player
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private InputReader playerInput;
        [SerializeField, Range(0f, 0.1f)] private float mouseSensitivity;
        [SerializeField] private float maxPitch, minPitch;
        [SerializeField] private float pitchRotationalSpeed;
        [SerializeField] private float yawRotationalSpeed;
        private float _yaw;
        private float _pitch;
        private Vector2 _mouse;
        private Vector2 _targetMouseDelta;
        private Vector2 _currentMouseDelta;
        private Vector2 _currentMouseDeltaVelocity;
        
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            _yaw += _mouse.x * yawRotationalSpeed * mouseSensitivity * Time.deltaTime;
            _pitch -= _mouse.y * pitchRotationalSpeed * mouseSensitivity * Time.deltaTime;
            _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);

            transform.parent.rotation = Quaternion.Euler(0.0f, _yaw, 0.0f);
            transform.localRotation = Quaternion.Euler(_pitch, 0.0f, 0.0f);
        }


        private void OnEnable()
        {
            playerInput.PlayerLookEvent += HandleLook;
            playerInput.PlayerCancelLookEvent += HandleCancelLook;

        }

        private void OnDisable()
        {
            playerInput.PlayerLookEvent -= HandleLook;
            playerInput.PlayerCancelLookEvent -= HandleCancelLook;

        }
        
        private void HandleLook(Vector2 direction)
        {
            _mouse = direction;
        }

        private void HandleCancelLook(Vector2 direction)
        {
            _mouse = direction;
        }
    }
}
