using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class Input
    {
        private readonly ThirdPersonGameInput _inputActions = new();

        public Input() => _inputActions.Enable();

        public Vector2 MovementInput => _inputActions.Player.Movement.ReadValue<Vector2>();
        public bool IsJumpPressed => _inputActions.Player.Jump.WasPressedThisFrame();
    }
}
