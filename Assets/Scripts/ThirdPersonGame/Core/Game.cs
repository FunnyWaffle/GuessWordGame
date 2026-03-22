using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

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

        public event Action<GameObject, GameObject> PlayerSpawned;

        public void Update()
        {
            var movementInput = _input?.MovementInput;
            _playerMover?.Move(movementInput.Value);
            _playerMover?.Jump(_input.IsJumpPressed);
            _playerRotator?.Update();
        }

        public PlayerMover CreatePlayerMover(CharacterController characterController,
            Animator animator,
            Transform playerRig,
            float movementVelocity,
            float jumpForce,
            float movementAcceleration,
            float movementDeceleration)
        {
            _playerMover = new PlayerMover(
                _input,
                characterController,
                animator,
                playerRig,
                movementVelocity,
                jumpForce,
                movementAcceleration,
                movementDeceleration);
            return _playerMover;
        }

        public PlayerRotator CreatePlayerRotator(Transform player, Transform camera)
        {
            _playerRotator = new PlayerRotator(player, camera);
            return _playerRotator;
        }

        private void SubscribeSceneLoadEvent()
        {
            _sceneLoader.SceneLoaded += HandleSceneLoad;
        }

        private async void HandleSceneLoad()
        {
            _input = new Input();
            _ = SpawnPlayerAsync();

        }

        private async Task SpawnPlayerAsync()
        {
            var playerView = await _spawner.SpawnAsync("Armature");
            await _spawner.SpawnAsync("Camera With Cinemachine Brain");
            var cinemachineCamera = await _spawner.SpawnAsync("FreeLook Cinemachine");

            PlayerSpawned?.Invoke(playerView, cinemachineCamera);
        }

        private void SpawnCoins()
        {

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
