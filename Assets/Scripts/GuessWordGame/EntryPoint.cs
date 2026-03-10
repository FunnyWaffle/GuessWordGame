using Assets.Scripts.GuessWordGame.UI;
using UnityEngine;

namespace Assets.Scripts.GuessWordGame
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameUI _gameUI;
        private void Start()
        {
            var game = new Game();
            var subscriber = new GameUIEventSubscriber(game, _gameUI);
        }
    }
}
