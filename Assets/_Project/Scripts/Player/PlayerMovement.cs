using ProjectBPop.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        private CharacterController _characterController;
        private HeadBobController _headBobController;
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

        public bool PlayerIsGrounded => _characterController.isGrounded;
        public bool PlayerIsRunning => _currentSpeed > walkSpeed;
        public bool MovingInputPressed { get; private set; }
        public bool CanMove { get;  set; }

        private void Awake()
        {
            SceneManager.sceneLoaded += SetPlayer;
            _characterController = GetComponent<CharacterController>();
            _playerTransform = transform;
            _headBobController = GetComponent<HeadBobController>();
            playerInput.PlayerMoveEvent += HandleMoveInput;
            playerInput.PlayerMoveCancelledEvent += HandleCancelMove;
            playerInput.PlayerJumpStartedEvent += HandleJumpInput;
            playerInput.PlayerJumpCancelledEvent += HandleCancelJumpInput;
            playerInput.PlayerRunEvent += HandleRunInput;
            playerInput.PlayerRunCancelEvent += HandleCancelRunInput;
            Physics.gravity = new Vector3(0f, -12f, 0f);
            
        }
        
        private void OnDisable()
        {
            playerInput.PlayerMoveEvent -= HandleMoveInput;
            playerInput.PlayerJumpStartedEvent -= HandleCancelJumpInput;
            playerInput.PlayerJumpCancelledEvent -= HandleCancelJumpInput;
            playerInput.PlayerRunEvent -= HandleRunInput;
            playerInput.PlayerRunCancelEvent -= HandleCancelRunInput;
            SceneManager.sceneLoaded -= SetPlayer;
        }

        private void Start()
        {
            _currentSpeed = walkSpeed;
            CanMove = true;
        }

        private void Update()
        {
            ApplyGravity();
            Jump();
            MovePlayer();
        }

        private void SetPlayer(Scene scene, LoadSceneMode mode)
        {
            var worldTransform = GameManager.Instance.GetHubInitialSpawnPosition();
            if (scene.name == "BlockHUBFinal")
            {
                worldTransform = GameManager.Instance.GetHubInitialSpawnPosition();
            }
            else if (scene.name == "Zone1")
            {
                worldTransform = GameManager.Instance.GetFirstZoneSpawnPosition();
            }
            else if (scene.name == "Zone2")
            {
                worldTransform = GameManager.Instance.GetSecondZoneSpawnPosition();
            }
            else if(scene.name == "Zone3")
            {
                worldTransform = GameManager.Instance.GetThirdZoneSpawnPosition();
            }
            else if (GameManager.Instance.SpawnMiddleHub)
            {
                worldTransform = GameManager.Instance.GetHubMiddleSpawnPosition();
            }
            else if (scene.name == "MainMenu")
            {
                worldTransform = GameManager.Instance.GetHubInitialSpawnPosition();
            }
            
            
            _characterController.enabled = false;
            transform.SetPositionAndRotation(worldTransform.Position, worldTransform.Rotation);
            _characterController.enabled = true;
        }

        #region Player Movement
        private void HandleMoveInput(Vector2 direction)
        {
            if(!CanMove) return;
            _targetDirection = direction;
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
            }
            
            _playerIsJumping = false;
        }
        #endregion
    }
}
