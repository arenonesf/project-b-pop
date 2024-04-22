using System;
using ProjectBPop.Input;
using UnityEngine;

namespace ProjectBPop.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private InputReader playerInput;
        [SerializeField] private float walkSpeed;
        [SerializeField] private float runSpeed;
        [SerializeField] private float jumpVelocity;
        [SerializeField] private Vector3 boxSizeCast;
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private float groundDrag;
        [SerializeField] private float airDrag;
        [SerializeField] private float movementMultiplier;
        [SerializeField] private float airMultiplier;
        private CharacterController _characterController;
        private Rigidbody _rigidBody;
        private Transform _playerTransform;
        private Vector2 _inputVector;
        private bool _playerIsJumping;
        private bool _playerIsGrounded;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerTransform = GetComponent<Transform>();
            playerInput.PlayerMoveEvent += HandleInput;
            playerInput.PlayerJumpStartedEvent += HandleStartJump;
            playerInput.PlayerJumpCancelledEvent += HandleCancelJump;
        }

        private void OnDisable()
        {
            playerInput.PlayerMoveEvent -= HandleInput;
            playerInput.PlayerJumpStartedEvent -= HandleCancelJump;
            playerInput.PlayerJumpCancelledEvent -= HandleCancelJump;
        }

        private void Update()
        {
            MovePlayer();
        }
        
        #region Player Movement
        private void HandleInput(Vector2 direction)
        {
            _inputVector = direction;
        }
        
        private void MovePlayer()
        {
            var velocity = (_playerTransform.forward * _inputVector.y + _playerTransform.right * _inputVector.x) * walkSpeed;
            _characterController.Move(velocity * Time.deltaTime);
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
