using System;
using System.Collections.Generic;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class Inventory
    {
        private readonly List<IReadOnlyCoin> _coins = new();

        public event Action<int> CoinsCountChanged;

        public void AddCoin(IReadOnlyCoin coin)
        {
            _coins.Add(coin);
            CoinsCountChanged?.Invoke(_coins.Count);
        }
    }
}
