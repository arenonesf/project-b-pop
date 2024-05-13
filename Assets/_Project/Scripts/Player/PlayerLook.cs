using System;
using ProjectBPop.Input;
using UnityEngine;

namespace ProjectBPop.Player
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private InputReader playerInput;
        [SerializeField] private float mouseSensitivity;
        [SerializeField] private float maxPitch, minPitch;
        [SerializeField] private float mouseSmoothTime = 0.05f;
        private float _mouseX;
        private float _mouseY;
        private Vector2 _targetMouseDelta;
        private Vector2 _currentMouseDelta;
        private Vector2 _currentMouseDeltaVelocity;
        private Transform _cameraHolder;
        
        private void Start()
        {
            _cameraHolder = transform;
            playerInput.PlayerLookEvent += HandleLook;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnDisable()
        {
            playerInput.PlayerLookEvent -= HandleLook;
        }
        
        private void Update()
        {
            UpdatePlayerLook();
        }
        
        private void HandleLook(Vector2 direction)
        {
            _mouseX -= direction.y * mouseSensitivity;
            _mouseY += direction.x * mouseSensitivity;
            _mouseX = Mathf.Clamp(_mouseX, minPitch, maxPitch);
        }

        private void UpdatePlayerLook()
        {
            _targetMouseDelta = new Vector2(_mouseX, _mouseY);

            _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, _targetMouseDelta,
                ref _currentMouseDeltaVelocity, mouseSmoothTime);
                
            _cameraHolder.transform.localRotation = Quaternion.Euler(_currentMouseDelta.x, 0f, 0f);
            transform.parent.rotation = Quaternion.Euler(0f, _currentMouseDelta.y, 0f);
        }
    }
}
