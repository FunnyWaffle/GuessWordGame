using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class PlayerRotator
    {
        private readonly Transform _player;
        private readonly Transform _camera;

        public PlayerRotator(Transform player, Transform camera)
        {
            _player = player;
            _camera = camera;
        }

        public void Update()
        {
            _player.forward = Vector3.ProjectOnPlane(_camera.forward, Vector3.up);
        }
    }
}
