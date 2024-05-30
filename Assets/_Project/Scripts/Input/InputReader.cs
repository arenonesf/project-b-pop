using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectBPop.Input
{
    [CreateAssetMenu(menuName = "InputReader")]
    public class InputReader : ScriptableObject, PlayerInput.INormalModeActions, PlayerInput.IUIActions
    {
        private PlayerInput _playerInput;

        public event Action<Vector2> PlayerMoveEvent;
        public event Action<Vector2> PlayerLookEvent;
        public event Action<Vector2> PlayerCancelLookEvent;
        public event Action PlayerMoveCancelledEvent;
        public event Action PlayerJumpStartedEvent;
        public event Action PlayerJumpCancelledEvent;
        public event Action PlayerInteractEvent;
        public event Action PlayerMagicInteractionEvent;
        public event Action PlayerRunEvent;
        public event Action PlayerRunCancelEvent;
        public event Action PlayerRespawnEvent;
        public event Action PlayerPauseGameEvent;
        public event Action PlayerResumeGameEvent;

        private void OnEnable()
        {
            if (_playerInput == null)
            {
                _playerInput = new PlayerInput();
                _playerInput.NormalMode.SetCallbacks(this);
                _playerInput.UI.SetCallbacks(this);
                SetGameplay();
            }
        }

        public void SetGameplay()
        {
            _playerInput.NormalMode.Enable();
            _playerInput.UI.Disable();
        }

        public void SetUI()
        {
            _playerInput.UI.Enable();
            _playerInput.NormalMode.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            PlayerMoveEvent?.Invoke(context.ReadValue<Vector2>());

            if (context.phase == InputActionPhase.Canceled)
            {
                PlayerMoveCancelledEvent?.Invoke();
            }
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
            if (context.phase == InputActionPhase.Performed)
            {
                PlayerLookEvent?.Invoke(context.ReadValue<Vector2>());
            }
            
            if (context.phase == InputActionPhase.Canceled)
            {
                PlayerCancelLookEvent?.Invoke(context.ReadValue<Vector2>());
            }
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

        public void OnRun(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PlayerRunEvent?.Invoke();
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                PlayerRunCancelEvent?.Invoke();
            }
        }

        public void OnPauseGame(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                PlayerPauseGameEvent?.Invoke();
        }

        public void OnNavigate(InputAction.CallbackContext context)
        {

        }

        public void OnSubmit(InputAction.CallbackContext context)
        {

        }

        public void OnCancel(InputAction.CallbackContext context)
        {

        }

        public void OnPoint(InputAction.CallbackContext context)
        {

        }

        public void OnClick(InputAction.CallbackContext context)
        {

        }

        public void OnScrollWheel(InputAction.CallbackContext context)
        {

        }

        public void OnMiddleClick(InputAction.CallbackContext context)
        {

        }

        public void OnRightClick(InputAction.CallbackContext context)
        {

        }

        public void OnTrackedDevicePosition(InputAction.CallbackContext context)
        {

        }

        public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
        {
            
        }

        public void OnResumeGame(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                PlayerResumeGameEvent?.Invoke();
        }
    }
}
    