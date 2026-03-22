using Assets.Scripts.ThirdPersonGame.Core;
using Assets.Scripts.ThirdPersonGame.View;

namespace Assets.Scripts.ThirdPersonGame.Controllers
{
    public class CoinController
    {
        private readonly Coin _coin;
        private readonly CoinView _coinView;

        public CoinController(Coin coin, CoinView coinView)
        {
            _coin = coin;
            _coinView = coinView;

            _coinView.CollisionPerformed += _coin.CollisionEnter;
        }
    }
}
