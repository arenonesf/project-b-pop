using ProjectBPop.Input;
using UnityEngine;

namespace ProjectBPop.Player
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private InputReader playerInput;
        [SerializeField] private float mouseSensitivity;
        [SerializeField] private float maxPitch, minPitch, maxYaw, minYaw;
        private float _mouseX;
        private float _mouseY;
        private Camera _playerCamera;
        
        // Start is called before the first frame update
        private void Start()
        {
            _playerCamera = GetComponentInChildren<Camera>();
            playerInput.PlayerLookEvent += HandleLook;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void HandleLook(Vector2 direction)
        {
            _mouseX -= direction.y * mouseSensitivity;
            _mouseY += direction.x * mouseSensitivity;
            _mouseX = Mathf.Clamp(_mouseX, minPitch, maxPitch);
            _mouseY = Mathf.Clamp(_mouseY, minYaw, maxYaw);
        }

        private void UpdatePlayerLook()
        {
            _playerCamera.transform.localRotation = Quaternion.Euler(_mouseX, 0f, 0f);
            transform.parent.rotation = Quaternion.Euler(0f, _mouseY, 0f);
        }

        // Update is called once per frame
        private void Update()
        {
            UpdatePlayerLook();
        }
    }
}
