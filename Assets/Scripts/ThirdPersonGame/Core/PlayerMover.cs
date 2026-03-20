using Assets.Scripts.ThirdPersonGame.View;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class PlayerMover
    {
        private readonly Transform _playerRig;
        private readonly Vector3 _playerRigOffset;

        private readonly CharacterController _characterController;
        private readonly Input _input;

        private readonly float _jumpForce;
        private float _currentVerticalVelocity;

        private readonly float _movementAcceleration;
        private readonly float _movementDeceleration;
        private readonly Vector2 _maxHorizontalVelocity;
        private Vector2 _currentHorizontalVelocity;

        public PlayerMover(PlayerView playerView, Input input)
        {
            _characterController = playerView.CharacterController;
            _playerRig = playerView.transform;
            _playerRigOffset = _characterController.transform.position - _playerRig.position;

            _input = input;

            _maxHorizontalVelocity = new Vector2(playerView.HorizontalVelocity, playerView.HorizontalVelocity);
            _jumpForce = playerView.JumpForce;
            _movementAcceleration = playerView.HorizontalAcceleration;
            _movementDeceleration = playerView.HorizontalDeceleration;
        }

        public void Update()
        {
            UpdateHorizontalVelocity();
            UpdateVerticalVelocity();
            Move();
            Jump();
            SynchronizeRigTransform();
        }

        private void UpdateHorizontalVelocity()
        {
            var input = _input.MovementInput;
            var direction = input.x * _playerRig.right +
                  input.y * _playerRig.forward;

            var targetVelocity = new Vector2(_maxHorizontalVelocity.x * direction.x, _maxHorizontalVelocity.y * direction.z);
            float currentAcceleration = GetAcceleration(input);

            _currentHorizontalVelocity = Vector2.MoveTowards(_currentHorizontalVelocity, targetVelocity,
                currentAcceleration * Time.deltaTime);
        }

        private float GetAcceleration(Vector2 input)
        {
            float currentAcceleration;
            if (input != Vector2.zero)
            {
                currentAcceleration = _characterController.isGrounded
                    ? _movementAcceleration
                    : _movementAcceleration * 0.9f;
            }
            else
            {
                currentAcceleration = _characterController.isGrounded
               ? _movementDeceleration
               : _movementDeceleration * 0.9f;
            }

            return currentAcceleration;
        }

        private void UpdateVerticalVelocity()
        {
            if (_characterController.isGrounded)
            {
                if (_currentVerticalVelocity > 0)
                    _currentVerticalVelocity = 0f;

                if (_input.IsJumpPressed)
                    _currentVerticalVelocity = _jumpForce;
            }
            else
            {
                _currentVerticalVelocity += Physics.gravity.y * Time.deltaTime;
            }
        }

        private void Move() =>
            _characterController.Move(new Vector3(_currentHorizontalVelocity.x, 0f, _currentHorizontalVelocity.y) * Time.deltaTime);

        private void Jump() =>
            _characterController.Move(_currentVerticalVelocity * Time.deltaTime * _playerRig.up);

        private void SynchronizeRigTransform()
        {
            _playerRig.SetPositionAndRotation(_characterController.transform.position - _playerRigOffset,
                _characterController.transform.rotation);
        }
    }
}
