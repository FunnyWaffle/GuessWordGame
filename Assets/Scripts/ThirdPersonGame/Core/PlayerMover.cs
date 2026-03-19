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

        private readonly float _velocity;

        public PlayerMover(PlayerView playerView, Input input, float velocity)
        {
            _characterController = playerView.CharacterController;
            _playerRig = playerView.transform;
            _playerRigOffset = _characterController.transform.position - _playerRig.position;

            _input = input;
            _velocity = velocity;
        }

        public void Update()
        {
            var input = _input.MovementInput;

            var displacement = _velocity * Time.deltaTime;

            var forwardDisplacement = displacement * input.y * _playerRig.forward;
            var sideDisplacement = displacement * input.x * _playerRig.right;
            _characterController.Move(sideDisplacement + forwardDisplacement);

            SynchronizeRigTransform();
        }

        private void SynchronizeRigTransform()
        {
            _playerRig.position = _characterController.transform.position - _playerRigOffset;
            _playerRig.rotation = _characterController.transform.rotation;
        }
    }
}
