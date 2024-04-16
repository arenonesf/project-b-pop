using ProjectBPop.Input;
using UnityEngine;

namespace ProjectBPop.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private InputReader playerInput;
        [SerializeField] private float speed;
        [SerializeField] private float jumpVelocity;
        [SerializeField] private Vector3 boxSizeCast;
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private float groundDrag;
        [SerializeField] private float airDrag;
        [SerializeField] private float movementMultiplier;
        [SerializeField] private float airMultiplier;
        private Rigidbody _rigidBody;
        private Transform _playerTransform;
        private Vector3 _direction;
        private Vector2 _inputVector;
        private bool _playerIsJumping;
        private bool _playerIsGrounded;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _playerTransform = GetComponent<Transform>();
            playerInput.PlayerMoveEvent += HandleMovement;
            playerInput.PlayerJumpStartedEvent += HandleStartJump;
            playerInput.PlayerJumpCancelledEvent += HandleCancelJump;
        }

        private void OnDisable()
        {
            playerInput.PlayerMoveEvent -= HandleMovement;
            playerInput.PlayerJumpStartedEvent -= HandleCancelJump;
            playerInput.PlayerJumpCancelledEvent -= HandleCancelJump;
        }
        
        private void FixedUpdate()
        {
            UpdateDirection();
            GroundDragApplicator();
            LimitVelocity();
            MovePlayer();
            GroundCheck();
        }
        
        #region Player Movement
        private void HandleMovement(Vector2 direction)
        {
            _inputVector = direction;
        }

        private void UpdateDirection()
        {
            _direction = _playerTransform.forward * _inputVector.y + _playerTransform.right * _inputVector.x;
        }
        private void MovePlayer()
        {
            if (_playerIsGrounded)
            {
                _rigidBody.AddForce(_direction.normalized * (speed * movementMultiplier), ForceMode.Acceleration);
            }
            else
            {
                _rigidBody.AddForce(_direction.normalized * (speed * movementMultiplier * airMultiplier), ForceMode.Acceleration);
            }
        }
        
        private void GroundDragApplicator()
        {
            _rigidBody.drag = _playerIsGrounded ? groundDrag : airDrag;
        }
        
        private void LimitVelocity()
        {
            var velocity = _rigidBody.velocity;
            var flatVelocity = new Vector3(velocity.x, 0f, velocity.z);

            if (!(flatVelocity.magnitude > speed)) return;
            var limitedVelocity = flatVelocity.normalized * speed;
            
            _rigidBody.velocity = new Vector3(limitedVelocity.x, _rigidBody.velocity.y, limitedVelocity.z);
        }
        #endregion

        #region Player Jump
        private void HandleStartJump()
        {
            _playerIsJumping = true;
            Jump();
        }

        private void HandleCancelJump()
        {
            _playerIsJumping = false;
        }
        
        private void Jump()
        {
            if(_playerIsJumping && _playerIsGrounded)
                _rigidBody.AddForce(0f, jumpVelocity, 0f, ForceMode.Impulse);
        }

        private void GroundCheck()
        {
            _playerIsGrounded = Physics.CheckBox(_playerTransform.position, boxSizeCast, _playerTransform.rotation, groundLayerMask);
        }
        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, boxSizeCast);
        }
    }
}
