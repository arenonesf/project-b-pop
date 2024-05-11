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
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private Vector3 offset;

        private CharacterController _characterController;
        private Transform _playerTransform;
        private bool _playerIsJumping;
        private float _currentSpeed;
        private Vector3 _playerVelocity;
        private float _verticalSpeed;
        private readonly Collider[] _groundHits = new Collider[1];
        private Vector2 _currentDirectionVelocity;
        private Vector2 _currentDirection;
        private Vector2 _targetDirection;
        private float _coyoteCounter;
        private HeadBobController _headBobController;

        public bool PlayerIsGrounded =>
            Physics.OverlapSphereNonAlloc(transform.position, 0.05f, _groundHits, groundLayerMask) > 0;

        public float PlayerSpeed => _currentSpeed;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerTransform = GetComponent<Transform>();
            _headBobController = GetComponent<HeadBobController>();
            playerInput.PlayerMoveEvent += HandleMoveInput;
            playerInput.PlayerJumpStartedEvent += HandleJumpInput;
            playerInput.PlayerJumpCancelledEvent += HandleCancelJumpInput;
            playerInput.PlayerRunEvent += HandleRunInput;
            playerInput.PlayerRunCancelEvent += HandleCancelRunInput;
            Physics.gravity = new Vector3(0f, -12f, 0f);
            Debug.Log(Physics.gravity);
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
            _headBobController.UpdateFrequency(false);
        }

        private void Update()
        {
            ApplyGravity();
            Jump();
            MovePlayer();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + offset, 0.05f);
        }

        #region Player Movement
        private void HandleMoveInput(Vector2 direction)
        {
            _targetDirection = direction;
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
                _verticalSpeed = -0.2f;
                _coyoteCounter = coyoteTime;
            }
            else
            {
                _verticalSpeed += 2 * Physics.gravity.y * Time.deltaTime;
                _coyoteCounter -= Time.deltaTime;
            }
        }

        private void HandleRunInput()
        {
            if(!PlayerIsGrounded) return;
            _currentSpeed = runSpeed;
            _headBobController.UpdateFrequency(true);
        }
        
        private void HandleCancelRunInput()
        {
            _currentSpeed = walkSpeed;
            _headBobController.UpdateFrequency(false);
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
            if (_playerIsJumping && _coyoteCounter > 0f)
            {
                _verticalSpeed = jumpSpeed;
            }
            
            _playerIsJumping = false;
        }
        #endregion
    }
}
