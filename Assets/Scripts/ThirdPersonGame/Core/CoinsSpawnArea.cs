using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class CoinsSpawnArea
    {
        private readonly List<Coin> _coins = new();

        private readonly BoxCollider _areaCollider;
        private readonly Transform _areaTransform;
        private readonly Spawner _spawner;

        private readonly int _coinsCount = 10;

        public CoinsSpawnArea(BoxCollider areaCollider, Spawner spawner)
        {
            _areaTransform = areaCollider.transform;
            _areaCollider = areaCollider;
            _spawner = spawner;
        }

        public IReadOnlyList<IReadOnlyCoin> Coins => _coins;

        public event Action<Coin, GameObject> CoinSpawned;
        public event Action<IReadOnlyCoin, Collider> CoinCollisionPerformed;

        public void Update()
        {
            foreach (var coin in _coins)
            {
                coin.Update();
            }
        }

        public async void CreateCoins()
        {
            var bounds = _areaCollider.bounds;
            var boundsMin = bounds.min;
            var boundsMax = bounds.max;
            for (int i = 0; i < _coinsCount; i++)
            {
                var position = GetRandomPosition(boundsMin, boundsMax);
                _ = SpawnCoinAsync(position);
            }
        }

        private Vector3 GetRandomPosition(Vector3 boundsMin, Vector3 boundsMax)
        {
            return new Vector3(
                UnityEngine.Random.Range(boundsMin.x, boundsMax.x),
                UnityEngine.Random.Range(boundsMin.y, boundsMax.y),
                UnityEngine.Random.Range(boundsMin.z, boundsMax.z));
        }

        private async Task SpawnCoinAsync(Vector3 position)
        {
            var coinView = await _spawner.SpawnAsync("Coin", position, _areaTransform);
            var coin = new Coin(coinView.transform);
            coin.CollisionPerformed += OnCoinCollisionPerform;

            _coins.Add(coin);
            CoinSpawned?.Invoke(coin, coinView);
        }

        private void OnCoinCollisionPerform(object sender, Collider collider)
        {
            CoinCollisionPerformed?.Invoke(sender as IReadOnlyCoin, collider);
        }
    }
}
