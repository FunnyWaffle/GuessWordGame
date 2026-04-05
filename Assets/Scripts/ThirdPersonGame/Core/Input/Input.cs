using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class Input
    {
        private readonly ThirdPersonGameInput _inputActions = new();

        public Input()
        {
            _inputActions.Enable();

            _inputActions.Player.FirstDanceAction.performed += HandleFirstDanceAction;
            _inputActions.Player.SecondDanceAction.performed += HandleSecondDanceAction;

            _inputActions.Player.ToggleDanceState.performed += HandleToggleDanceStateAction;
        }

        public Vector2 MovementInput => _inputActions.Player.Movement.ReadValue<Vector2>();
        public bool IsJumpPressed => _inputActions.Player.Jump.WasPressedThisFrame();
        public bool IsStartDanceButtonPressed => _inputActions.Player.ToggleDanceState.WasPressedThisFrame();

        public event Action ToggleDanceStateActionPerformed;
        public event Action<int> DanceActionPerformed;

        private void HandleToggleDanceStateAction(InputAction.CallbackContext context) => ToggleDanceStateActionPerformed?.Invoke();

        private void HandleFirstDanceAction(InputAction.CallbackContext context) => OnDanceAction(0);

        private void HandleSecondDanceAction(InputAction.CallbackContext context) => OnDanceAction(1);

        private void OnDanceAction(int value) => DanceActionPerformed?.Invoke(value);
    }
}
