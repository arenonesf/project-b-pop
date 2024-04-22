using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectBPop.Input
{
    [CreateAssetMenu(menuName = "InputReader")]
    public class InputReader : ScriptableObject, PlayerInput.INormalModeActions
    {
        private PlayerInput _playerInput;

        public event Action<Vector2> PlayerMoveEvent;
        public event Action<Vector2> PlayerLookEvent;
        public event Action PlayerJumpStartedEvent;
        public event Action PlayerJumpCancelledEvent;
        public event Action PlayerInteractEvent;
        public event Action PlayerMagicInteractionEvent;
        public event Action PlayerRespawnEvent;

        private void OnEnable()
        {
            if (_playerInput == null)
            {
                _playerInput = new PlayerInput();
                _playerInput.NormalMode.SetCallbacks(this);
                SetNormalMode();
            }
        }

        public void SetNormalMode()
        {
            _playerInput.NormalMode.Enable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            PlayerMoveEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Performed)
            {
                PlayerJumpStartedEvent?.Invoke();
            }
            
            if(context.phase == InputActionPhase.Canceled)
            {
                PlayerJumpCancelledEvent?.Invoke();
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                PlayerInteractEvent?.Invoke();
            }
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            PlayerLookEvent?.Invoke(context.ReadValue<Vector2>());
        }

       

        public void OnRespawn(InputAction.CallbackContext context)
        {
            PlayerRespawnEvent?.Invoke();
        }

        public void OnMagicInteraction(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PlayerMagicInteractionEvent?.Invoke();
            }
        }
    }
}
