using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class Player
    {
        private readonly Input _input;

        private readonly PlayerMover _playerMover;
        private readonly PlayerRotator _playerRotator;
        private readonly Inventory _inventory;

        private readonly Collider _collider;

        public Player(PlayerMover playerMover,
            PlayerRotator playerRotator,
            Inventory inventory,
            Collider collider,
            Input input)
        {
            _playerMover = playerMover;
            _playerRotator = playerRotator;
            _inventory = inventory;
            _collider = collider;
            _input = input;
        }

        public void Update()
        {
            var movementInput = _input.MovementInput;

            _playerMover.Move(movementInput);
            _playerMover.Jump(_input.IsJumpPressed);
            _playerRotator.Update();

            var isStartDanceButtonPressed = _input.IsStartDanceButtonPressed;


        }

        public bool IsColliderMatch(Collider collider)
        {
            return _collider == collider;
        }

        public void AddCoinToInventory(IReadOnlyCoin coin)
        {
            _inventory.AddCoin(coin);
        }
    }
}
