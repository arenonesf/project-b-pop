using ProjectBPop.Input;
using UnityEngine;

namespace ProjectBPop.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private InputReader playerInput;
        [SerializeField] private float walkSpeed;
        [SerializeField] private float runSpeed;
        [SerializeField] private float jumpSpeed;
        [SerializeField] private LayerMask groundLayerMask;
        private CharacterController _characterController;
        private Rigidbody _rigidBody;
        private Transform _playerTransform;
        private Vector2 _inputVector;
        private bool _playerIsJumping;
        private bool _playerIsGrounded;
        private float _currentSpeed;
        private Vector3 _playerVelocity;
        private float _verticalSpeed;
        public Vector3 PlayerVelocity => _playerVelocity;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerTransform = GetComponent<Transform>();
            playerInput.PlayerMoveEvent += HandleMoveInput;
            playerInput.PlayerJumpStartedEvent += HandleJumpInput;
            playerInput.PlayerJumpCancelledEvent += HandleCancelJumpInput;
            playerInput.PlayerRunEvent += HandleRunInput;
            playerInput.PlayerRunCancelEvent += HandleCancelRunInput;
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
            if (_playerIsGrounded)
            {
                _verticalSpeed = 0f;
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
            if (_playerIsJumping && _playerIsGrounded)
            {
                _verticalSpeed = jumpSpeed;
            }
            
            if (_verticalSpeed <= 0f && _playerIsJumping)
            {
                _playerIsJumping = false;
            }
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Debug.Log(hit.collider.name);
            transform.parent = hit.transform;
        }

        #endregion
    }
}
