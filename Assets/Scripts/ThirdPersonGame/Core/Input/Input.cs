using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class Input
    {
        private readonly PlayerInput _inputActions = new();

        public Input() => _inputActions.Enable();

        public Vector2 MovementInput => _inputActions.Player.Movement.ReadValue<Vector2>();
    }
}
