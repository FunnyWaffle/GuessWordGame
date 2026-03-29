using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class CoinPicker
    {
        private CoinsSpawnArea _coinsSpawnArea;
        private Player _player;

        public void SetCoinArea(CoinsSpawnArea area)
        {
            _coinsSpawnArea = area;

            _coinsSpawnArea.CoinCollisionPerformed += HandleCoinCollision;
        }

        public void SetInventoryOwner(Player player)
        {
            _player = player;
        }

        private void HandleCoinCollision(IReadOnlyCoin coin, Collider collider)
        {
            if (!_player.IsColliderMatch(collider))
                return;

            _player.AddCoinToInventory(coin);
        }
    }
}
