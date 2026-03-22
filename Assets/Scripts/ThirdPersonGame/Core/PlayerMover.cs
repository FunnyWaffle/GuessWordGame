using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class PlayerMover
    {
        private readonly Transform _playerRig;
        private readonly Vector3 _playerRigOffset;

        private readonly CharacterController _characterController;
        private readonly Animator _animator;
        private readonly Input _input;

        private readonly float _jumpForce;
        private float _currentJumpForce;

        private readonly float _movementAcceleration;
        private readonly float _movementDeceleration;
        private Vector3 _maxMovementVelocity;
        private Vector3 _currentMovementVelocity;

        public PlayerMover(Input input,
            CharacterController characterController,
            Animator animator,
            Transform playerRig,
            float movementVelocity,
            float jumpForce,
            float movementAcceleration,
            float movementDeceleration)
        {
            _input = input;

            _characterController = characterController;
            _animator = animator;
            _playerRig = playerRig;
            _playerRigOffset = _characterController.transform.position - _playerRig.position;


            _maxMovementVelocity = new Vector3(movementVelocity, 0f, movementVelocity);
            _jumpForce = jumpForce;
            _movementAcceleration = movementAcceleration;
            _movementDeceleration = movementDeceleration;
        }

        public void SetMovementVelocity(float movementVelocity)
        {
            _maxMovementVelocity = new Vector3(movementVelocity, 0f, movementVelocity);
        }

        public void Move(Vector2 input)
        {
            UpdateMovementVelocity(input);
            Move();
            SynchronizeRigTransform();
            EnableMovementAnimation();
        }

        public void Jump(bool isJumping)
        {
            UpdateJumpVelocity(isJumping);
            Jump();
            SynchronizeRigTransform();
            EnableJumpAnimation(isJumping);
        }

        private void UpdateMovementVelocity(Vector2 input)
        {
            var direction = input.x * _playerRig.right +
                  input.y * _playerRig.forward;

            var targetVelocity = Vector3.Scale(direction, _maxMovementVelocity);
            float currentAcceleration = GetAcceleration(input);

            _currentMovementVelocity = Vector3.MoveTowards(_currentMovementVelocity, targetVelocity,
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

        private void UpdateJumpVelocity(bool isJumping)
        {
            if (_characterController.isGrounded)
            {
                if (_currentJumpForce < 0)
                    _currentJumpForce = 0f;

                if (isJumping)
                    _currentJumpForce = _jumpForce;
            }
            else
            {
                _currentJumpForce += Physics.gravity.y * Time.deltaTime;
            }
        }

        private void Move() =>
            _characterController.Move(_currentMovementVelocity * Time.deltaTime);

        private void Jump() =>
            _characterController.Move(_currentJumpForce * Time.deltaTime * _playerRig.up);

        private void SynchronizeRigTransform()
        {
            _playerRig.SetPositionAndRotation(_characterController.transform.position - _playerRigOffset,
                _characterController.transform.rotation);
        }

        private void EnableMovementAnimation()
        {
            var localVelocity = _playerRig.InverseTransformDirection(_currentMovementVelocity);
            _animator.SetFloat("ForwardSpeed", localVelocity.z / _maxMovementVelocity.z);

        }

        private void EnableJumpAnimation(bool isJumping)
        {
            if (isJumping)
                _animator.SetTrigger("IsJumpReleased");

            _animator.SetBool("Grounded", _characterController.isGrounded);
        }
    }
}
