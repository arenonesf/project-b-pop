using System;
using ProjectBPop.Input;
using UnityEngine;

namespace ProjectBPop.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private InputReader playerInput;
        [SerializeField] private float speed;
        private Rigidbody _rigidBody;
        private Transform _playerTransform;
        private Vector3 _direction;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _playerTransform = GetComponent<Transform>();
            playerInput.PlayerMoveEvent += HandleMovement;
        }

        private void HandleMovement(Vector2 direction)
        {
            _direction = transform.forward * direction.y + _playerTransform.right * direction.x;
        }

        private void MovePlayer()
        {
            _rigidBody.AddForce(_direction * speed, ForceMode.Acceleration);
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }
    }
}
