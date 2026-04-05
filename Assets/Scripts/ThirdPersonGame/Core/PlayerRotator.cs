using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class PlayerRotator
    {
        private readonly Transform _player;
        private readonly Transform _camera;
        private readonly Animator _animator;


        private float _lastFrameYaw;

        public PlayerRotator(Transform player, Transform camera, Animator animator)
        {
            _player = player;
            _camera = camera;
            _animator = animator;
        }

        public void Update()
        {
            var currentYaw = _camera.eulerAngles.y;
            var yawDelta = Mathf.DeltaAngle(_lastFrameYaw, currentYaw);
            _lastFrameYaw = currentYaw;

            var targetDirection = Vector3.ProjectOnPlane(_camera.forward, Vector3.up);

            _player.forward = Vector3.Slerp(_player.forward, targetDirection, Time.deltaTime * 10f);

            _animator.SetFloat(MovementAnimatorParameters.RotationAngle, yawDelta / Time.deltaTime);
        }
    }
}
