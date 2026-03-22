using Assets.Scripts.ThirdPersonGame.Core;
using Assets.Scripts.ThirdPersonGame.View;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Controllers
{
    public class CoinsSpawnAreaController
    {
        private readonly CoinsSpawnArea _coinsSpawnArea;
        private readonly CoinsSpawnAreaView _coinsSpawnAreaView;

        private readonly List<CoinController> _coinControllers;

        public CoinsSpawnAreaController(CoinsSpawnArea coinsSpawnArea, CoinsSpawnAreaView coinsSpawnAreaView)
        {
            _coinsSpawnArea = coinsSpawnArea;
            _coinsSpawnAreaView = coinsSpawnAreaView;

            _coinsSpawnAreaView.TriggerPerformed += _coinsSpawnArea.CreateCoins;

            _coinsSpawnArea.CoinSpawned += HandleCoinSpawn;
        }

        private void HandleCoinSpawn(Coin coin, GameObject coinObject)
        {
            var coinView = coinObject.GetComponent<CoinView>();
            _coinControllers.Add(new CoinController(coin, coinView));
        }
    }
}
