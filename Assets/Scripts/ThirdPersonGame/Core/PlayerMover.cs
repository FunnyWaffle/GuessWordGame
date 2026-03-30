using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class PlayerMover
    {
        private const float AIR_RESPONSIVENESS = 0.1f;

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

        public void SetMaxMovementVelocity(float maxMovementVelocity)
        {
            _maxMovementVelocity = new Vector3(maxMovementVelocity, 0f, maxMovementVelocity);
        }

        public void Move(Vector2 input)
        {
            UpdateMovementVelocity(input);
            ReleaseMove();
            SynchronizeRigTransform();
            EnableMovementAnimation();
        }

        public void Jump(bool isJumping)
        {
            var isGrounded = IsGrounded();
            UpdateJumpVelocity(isGrounded, isJumping);
            ReleaseJump();
            SynchronizeRigTransform();
            EnableJumpAnimation(isGrounded, isJumping);
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
            var isGrounded = IsGrounded();
            if (input == Vector2.zero)
            {
                currentAcceleration = isGrounded
               ? _movementDeceleration
               : _movementDeceleration * AIR_RESPONSIVENESS;
            }
            else
            {
                currentAcceleration = isGrounded
                    ? _movementAcceleration
                    : _movementAcceleration * AIR_RESPONSIVENESS;
            }

            return currentAcceleration;
        }

        private bool IsGrounded()
        {
            var isHited = Physics.Raycast(_playerRig.position, Vector3.down,
                _characterController.skinWidth + float.Epsilon,
                Layers.GetLayerIndex(LayerName.RaycastIgnore),
                 QueryTriggerInteraction.Ignore);
            return isHited;
        }

        private void UpdateJumpVelocity(bool isGrounded, bool isJumping)
        {
            if (isGrounded)
            {
                if (_currentJumpForce < 0)
                    _currentJumpForce = 0f;

                if (isJumping)
                {
                    _currentJumpForce = _jumpForce;
                }
            }
            else
            {
                _currentJumpForce += Physics.gravity.y * Time.deltaTime;
            }
        }

        private void ReleaseMove() =>
            _characterController.Move(_currentMovementVelocity * Time.deltaTime);

        private void ReleaseJump() =>
            _characterController.Move(_currentJumpForce * Time.deltaTime * _playerRig.up);

        private void SynchronizeRigTransform()
        {
            _playerRig.SetPositionAndRotation(_characterController.transform.position - _playerRigOffset,
                _characterController.transform.rotation);
        }

        private void EnableMovementAnimation()
        {
            var localVelocity = _playerRig.InverseTransformDirection(_currentMovementVelocity);
            _animator.SetFloat(MovementAnimatorParameters.ForwardSpeed, localVelocity.z / _maxMovementVelocity.z);
            _animator.SetFloat(MovementAnimatorParameters.SideSpeed, localVelocity.x / _maxMovementVelocity.x);
        }

        private void EnableJumpAnimation(bool isGrounded, bool isJumping)
        {
            if (isJumping)
                _animator.SetTrigger(MovementAnimatorParameters.IsJumpReleased);

            _animator.SetBool(MovementAnimatorParameters.Grounded, isGrounded);
        }
    }
}
