using Assets.Scripts.ThirdPersonGame.View;
using System.Collections.Generic;
using Unity.Cinemachine;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class Game
    {
        private readonly SceneLoader _sceneLoader = new();
        private readonly Spawner _spawner = new();

        private PlayerMover _playerMover;
        private PlayerRotator _playerRotator;
        private Input _input;

        private readonly List<LevelSelectionButton> _levelSelectionButtons = new();

        public Game()
        {
            SubscribeSceneLoadEvent();
            CreateLevelSelectionButtonts();
            SubscribeButtonEvents();
        }

        public IEnumerable<LevelSelectionButton> LevelSelectionButtons => _levelSelectionButtons;

        public void Update()
        {
            _playerMover?.Update();
            _playerRotator?.Update();
        }

        private void SubscribeSceneLoadEvent()
        {
            _sceneLoader.SceneLoaded += HandleSceneLoad;
        }

        private async void HandleSceneLoad()
        {
            var playerView = await _spawner.SpawnAsync<PlayerView>("Player");

            await _spawner.SpawnAsync("Camera With Cinemachine Brain");
            var cinemachineCamera = await _spawner.SpawnAsync<CinemachineCamera>("FreeLook Cinemachine");
            cinemachineCamera.Target.TrackingTarget = playerView.CharacterController.transform;

            _input = new Input();
            _playerMover = new PlayerMover(playerView, _input, playerView.Velocity);
            _playerRotator = new PlayerRotator(playerView.transform, cinemachineCamera.transform);
        }

        private void CreateLevelSelectionButtonts()
        {
            var entryPoint = 1;
            for (int i = 0 + entryPoint; i <= _sceneLoader.MaxSceneId; i++)
            {
                var levelName = "Level ";
                _levelSelectionButtons.Add(new LevelSelectionButton(i, levelName));
            }
        }

        private void SubscribeButtonEvents()
        {
            foreach (var button in _levelSelectionButtons)
            {
                button.ButtonClicked += _sceneLoader.LoadScene;
            }
        }
    }
}
