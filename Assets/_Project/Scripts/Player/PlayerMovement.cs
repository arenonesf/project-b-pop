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
        private Rigidbody _rigidBody;
        private Transform _playerTransform;
        private Vector3 _direction;
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
            MovePlayer();
            GroundCheck();
            Jump();
        }
        
        #region Player Movement
        private void HandleMovement(Vector2 direction)
        {
            _direction = _playerTransform.forward * direction.y + _playerTransform.right * direction.x;
        }

        private void MovePlayer()
        {
            _rigidBody.AddForce(_direction * speed, ForceMode.Acceleration);
        }
        #endregion
        
        #region Player Jump
        private void HandleStartJump()
        {
            _playerIsJumping = true;
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
