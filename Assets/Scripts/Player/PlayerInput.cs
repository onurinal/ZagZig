using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ZagZig.Player
{
    [CreateAssetMenu(fileName = "PlayerInputActions", menuName = "ZagZig/Player/PlayerInputActions")]
    public class PlayerInput : ScriptableObject
    {
        private PlayerInputActions playerInputActions;

        public event Action OnTap;

        private void OnEnable()
        {
            if (playerInputActions == null)
            {
                playerInputActions = new PlayerInputActions();
            }

            playerInputActions.Player.Tap.performed += HandleMovement;
            playerInputActions.Player.Enable();
        }

        private void OnDisable()
        {
            if (playerInputActions != null)
            {
                playerInputActions.Player.Disable();
                playerInputActions.Player.Tap.performed -= HandleMovement;
            }
        }

        private void HandleMovement(InputAction.CallbackContext context)
        {
            OnTap?.Invoke();
        }
    }
}