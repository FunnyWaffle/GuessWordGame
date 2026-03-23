using Assets.Scripts.ThirdPersonGame.Controllers;
using Assets.Scripts.ThirdPersonGame.Core;
using Assets.Scripts.ThirdPersonGame.View.UI.EntryPoint;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private EntryPointUIRoot _uIRoot;
        private Game _game;

        private void Start()
        {
            DontDestroyOnLoad(this);

            _uIRoot.Initialize();

            _game = new Game();

            new GameController(_game, _uIRoot);
        }

        private void Update()
        {
            _game.Update();
        }
    }
}