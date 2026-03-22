using Assets.Scripts.ThirdPersonGame.Core;
using Assets.Scripts.ThirdPersonGame.View;

namespace Assets.Scripts.ThirdPersonGame.Controllers
{
    public class PlayerController
    {
        private readonly PlayerView _playerView;
        private readonly PlayerMover _playerMover;
        private readonly PlayerRotator _playerRotator;

        public PlayerController(PlayerView playerView, PlayerMover playerMover, PlayerRotator playerRotator)
        {
            _playerView = playerView;
            _playerMover = playerMover;
            _playerRotator = playerRotator;

            _playerView.MovementVelocityChanged += _playerMover.SetMovementVelocity;
        }
    }
}
