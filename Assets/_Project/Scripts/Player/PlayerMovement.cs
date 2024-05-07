using ProjectBPop.Input;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace ProjectBPop.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private InputReader playerInput;
        [SerializeField] private float walkSpeed;
        [SerializeField] private float runSpeed;
        [SerializeField] private float jumpSpeed;
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private float walkBobSpeed = 14f;
        [SerializeField] private float walkBobAmount = 0.05f;
        [SerializeField] private bool enableHeadBob = true;
        private float _timer;
        private Camera _playerCamera;
        private CharacterController _characterController;
        private Transform _playerTransform;
        private Vector2 _inputVector;
        private bool _playerIsJumping;
        private bool _playerIsGrounded;
        private float _currentSpeed;
        private Vector3 _playerVelocity;
        private float _verticalSpeed;
        public float CurrentSpeed => _currentSpeed;
        
        public Vector3 PlayerVelocity => _playerVelocity;
        public bool PlayerIsGrounded => _playerIsGrounded;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerTransform = GetComponent<Transform>();
            playerInput.PlayerMoveEvent += HandleMoveInput;
            playerInput.PlayerJumpStartedEvent += HandleJumpInput;
            playerInput.PlayerJumpCancelledEvent += HandleCancelJumpInput;
            playerInput.PlayerRunEvent += HandleRunInput;
            playerInput.PlayerRunCancelEvent += HandleCancelRunInput;
            _playerCamera = GetComponentInChildren<Camera>();
        }

        private void OnDisable()
        {
            playerInput.PlayerMoveEvent -= HandleMoveInput;
            playerInput.PlayerJumpStartedEvent -= HandleCancelJumpInput;
            playerInput.PlayerJumpCancelledEvent -= HandleCancelJumpInput;
            playerInput.PlayerRunEvent -= HandleRunInput;
            playerInput.PlayerRunCancelEvent -= HandleCancelRunInput;
        }

        private void Start()
        {
            _currentSpeed = walkSpeed;
        }

        private void Update()
        {
            ApplyGravity();
            Jump();
            MovePlayer();
            if (enableHeadBob)
            {
                HandleHeadBob();
            }
        }

        private void HandleHeadBob()
        {
            if (!_characterController.isGrounded) return;
            if (Mathf.Abs(_inputVector.x) > 0.1f || Mathf.Abs(_inputVector.y) > 0.1f)
            {
                _timer += Time.deltaTime * walkBobSpeed;
                _playerCamera.transform.localPosition = new Vector3(_playerCamera.transform.localPosition.x
                    +Mathf.Sin(_timer) * walkBobAmount * Time.deltaTime,
                    _playerCamera.transform.localPosition.y + Mathf.Sin(_timer) * walkBobAmount * Time.deltaTime,
                    _playerCamera.transform.localPosition.z);
            }
            else
            {
                _timer = 0f;
            }

        }

        #region Player Movement
        private void HandleMoveInput(Vector2 direction)
        {
            _inputVector = direction;
        }
        
        private void MovePlayer()
        {
            _playerVelocity = (_playerTransform.forward * _inputVector.y + _playerTransform.right * _inputVector.x) *
                              _currentSpeed;
            _playerVelocity.y = _verticalSpeed;
            _characterController.Move(_playerVelocity * Time.deltaTime);
        }

        private void ApplyGravity()
        {
            if (_characterController.isGrounded && (_characterController.collisionFlags & CollisionFlags.Below) != 0)
            {
                _verticalSpeed = -0.2f;
            }
            else
            {
                _verticalSpeed += Physics.gravity.y * Time.deltaTime;
            }
        }

        private void HandleRunInput()
        {
            _currentSpeed = runSpeed;
        }
        
        private void HandleCancelRunInput()
        {
            _currentSpeed = walkSpeed;
        }
        #endregion

        #region Player Jump
        private void HandleJumpInput()
        {
            _playerIsJumping = true;
        }

        private void HandleCancelJumpInput()
        {
            _playerIsJumping = false;
        }
        
        private void Jump()
        {
            if (_playerIsJumping && _characterController.isGrounded)
            {
                _verticalSpeed = jumpSpeed;
            }
            
            if (_verticalSpeed <= 0f && _playerIsJumping)
            {
                _playerIsJumping = false;
            }
        }
        #endregion
    }
}
