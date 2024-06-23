using System;
using ProjectBPop.Input;
using UnityEngine;

namespace ProjectBPop.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private InputReader playerInput;
        [SerializeField, Range(0f, 0.5f)] private float moveSmoothTime; 
        [SerializeField] private float walkSpeed;
        [SerializeField] private float runSpeed;
        [SerializeField] private float jumpSpeed;
        [SerializeField] private float coyoteTime;
        [SerializeField] private float antiBump = -5.0f;
        [Space(10), Header("Camera Settings")]
        [SerializeField] private Transform pitchController;
        [SerializeField, Range(0f, 0.1f)] private float mouseSensitivity = 0.05f;
        [SerializeField] private float maxPitch = 90f;
        [SerializeField] private float minPitch = -90f;
        [SerializeField] private float pitchRotationalSpeed = 360f;
        [SerializeField] private float yawRotationalSpeed = 720f;
        
        private float _yaw;
        private float _pitch;
        private Vector2 _mouse;
        private CharacterController _characterController;
        private HeadBobController _headBobController;
        private PlayerInteract _playerInteract;
        private Transform _playerTransform;
        private bool _playerIsJumping;
        private bool _playerOnAir;
        private float _currentSpeed;
        private Vector3 _playerVelocity;
        private float _verticalSpeed;
        private Vector2 _currentDirectionVelocity;
        private Vector2 _currentDirection;
        private Vector2 _targetDirection;
        private float _coyoteCounter;
        public bool Rotated { get; set; }

        public Action OnJump;
        public bool PlayerIsGrounded => _characterController.isGrounded;
        public bool PlayerIsRunning => _currentSpeed > walkSpeed;
        public bool MovingInputPressed { get; private set; }
        public bool CanMove { get;  set; }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerTransform = transform;
            _headBobController = GetComponent<HeadBobController>();
            _playerInteract = GetComponent<PlayerInteract>();
            Physics.gravity = new Vector3(0f, -12f, 0f);
        }

        private void OnEnable()
        {
            playerInput.PlayerMoveEvent += HandleMoveInput;
            playerInput.PlayerMoveCancelledEvent += HandleCancelMove;
            playerInput.PlayerJumpStartedEvent += HandleJumpInput;
            playerInput.PlayerJumpCancelledEvent += HandleCancelJumpInput;
            playerInput.PlayerRunEvent += HandleRunInput;
            playerInput.PlayerRunCancelEvent += HandleCancelRunInput;
            playerInput.PlayerLookEvent += HandleLook;
            playerInput.PlayerCancelLookEvent += HandleCancelLook;
        }

        private void OnDisable()
        {
            playerInput.PlayerMoveEvent -= HandleMoveInput;
            playerInput.PlayerJumpStartedEvent -= HandleCancelJumpInput;
            playerInput.PlayerJumpCancelledEvent -= HandleCancelJumpInput;
            playerInput.PlayerRunEvent -= HandleRunInput;
            playerInput.PlayerRunCancelEvent -= HandleCancelRunInput;
            playerInput.PlayerLookEvent -= HandleLook;
            playerInput.PlayerCancelLookEvent -= HandleCancelLook;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _currentSpeed = walkSpeed;
            CanMove = true;
        }

        private void Update()
        {
            
            Jump();
            if(CanMove){
                ApplyGravity();
                _yaw += _mouse.x * yawRotationalSpeed * mouseSensitivity * Time.deltaTime;
                if (Rotated)
                {
                    _pitch += _mouse.y * pitchRotationalSpeed * mouseSensitivity * Time.deltaTime;
                }
                else
                {
                    _pitch -= _mouse.y * pitchRotationalSpeed * mouseSensitivity * Time.deltaTime;
                }
                _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);
                transform.rotation = Quaternion.Euler(0.0f,_yaw, 0.0f);
                pitchController.localRotation = Quaternion.Euler(_pitch, 0.0f, 0.0f);  
                MovePlayer();
            }
        }

        private void HandleLook(Vector2 direction)
        {
            _mouse = direction;
        }

        private void HandleCancelLook(Vector2 direction)
        {
            _mouse = direction;
        }

        #region Player Movement
        private void HandleMoveInput(Vector2 direction)
        {
            _targetDirection = Rotated ? -direction : direction;
            MovingInputPressed = true;
        }

        private void HandleCancelMove()
        {
            MovingInputPressed = false;
        }
        
        private void MovePlayer()
        {
            _currentDirection = Vector2.SmoothDamp(_currentDirection, _targetDirection,
                ref _currentDirectionVelocity, moveSmoothTime);
            _playerVelocity = (_playerTransform.forward * _currentDirection.y + _playerTransform.right * _currentDirection.x) *
                              _currentSpeed;
            _playerVelocity.y = _verticalSpeed;
            
            _characterController.Move(_playerVelocity * Time.deltaTime);
        }

        private void ApplyGravity()
        {
            if (PlayerIsGrounded)
            {
                _verticalSpeed = antiBump;
                _coyoteCounter = coyoteTime;
            }
            else
            {
                _verticalSpeed += Physics.gravity.y * Time.deltaTime;
                _coyoteCounter -= Time.deltaTime;
            }
        }

        private void HandleRunInput()
        {
            if(!PlayerIsGrounded) return;
            _currentSpeed = runSpeed;
            _headBobController.UpdateFrequency(PlayerIsRunning);
        }
        
        private void HandleCancelRunInput()
        {
            if(!PlayerIsGrounded) return;
            _currentSpeed = walkSpeed;
            _headBobController.UpdateFrequency(PlayerIsRunning);
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
            if (_characterController.isGrounded) _playerOnAir = false;
            if (_playerIsJumping && _coyoteCounter > 0f && !_playerOnAir)
            {
                _verticalSpeed = jumpSpeed;
                _playerOnAir = true;
                OnJump?.Invoke();
            }
            
            _playerIsJumping = false;
        }
        #endregion
    }
}
