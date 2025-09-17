using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MGMG.Input
{
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/PlayerInputSO")]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        public event Action MoveEvent;
        public Vector2 InputDirection { get; private set; }
        public Vector2 LastInputDirection { get; private set; }
        public Vector3 MousePos { get; private set; }

        private Controls _controls;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            if (context.canceled)
                LastInputDirection = InputDirection;

            MoveEvent?.Invoke();
            InputDirection = context.ReadValue<Vector2>();

        }

        

        public void OnMousePos(InputAction.CallbackContext context)
        {
            MousePos = context.ReadValue<Vector2>();
        }

    }
}