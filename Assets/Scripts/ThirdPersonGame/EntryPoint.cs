using Assets.Scripts.ThirdPersonGame.Controllers;
using Assets.Scripts.ThirdPersonGame.Core;
using Assets.Scripts.ThirdPersonGame.View.UI;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private UIRoot _uIRoot;
        private Game _game;

        private void Start()
        {
            DontDestroyOnLoad(this);

            _uIRoot.Initialize();

            _game = new Game();

            var uIMenusSwitcher = new UIMenusSwitcher();

            var controllersBuilder = new ControllersBuilder();
            controllersBuilder.CreateControllers(_game, _uIRoot, uIMenusSwitcher);
        }

        private void Update()
        {
            _game.Update();
        }
    }
}