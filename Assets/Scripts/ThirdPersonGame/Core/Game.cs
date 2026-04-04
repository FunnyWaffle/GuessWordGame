using Assets.Scripts.ThirdPersonGame.Core.Assets;
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
        private readonly CoinPicker _coinPicker = new();

        private Input _input;
        private CoinsSpawnArea _coinsSpawnArea;
        private Player _player;
        private readonly List<LevelSelectionButton> _levelSelectionButtons = new();

        public Game()
        {
            SubscribeSceneLoadEvent();
            CreateLevelSelectionButtonts();
            SubscribeButtonEvents();
        }

        public IEnumerable<LevelSelectionButton> LevelSelectionButtons => _levelSelectionButtons;

        public event Action SceneLoaded;
        public event Action<GameObject, GameObject> PlayerSpawned;
        public event Action<GameObject> CoinsSpawnAreaSpawned;

        public void Update()
        {
            _player?.Update();
            _coinsSpawnArea?.Update();
        }

        public PlayerMover CreatePlayerMover(CharacterController characterController,
            Animator animator,
            Transform playerRig,
            float movementVelocity,
            float jumpForce,
            float movementAcceleration,
            float movementDeceleration)
        {
            return new PlayerMover(
                _input,
                characterController,
                animator,
                playerRig,
                movementVelocity,
                jumpForce,
                movementAcceleration,
                movementDeceleration);
        }

        public Player CreatePlayer(PlayerMover playerMover,
            PlayerRotator playerRotator,
            Inventory inventory,
            Collider collider,
            Animator animator)
        {
            _player = new Player(playerMover, playerRotator, inventory, collider, _input, animator);
            _coinPicker.SetInventoryOwner(_player);
            return _player;
        }

        public PlayerRotator CreatePlayerRotator(Transform player, Transform camera,
            Animator animator)
        {
            return new PlayerRotator(player, camera, animator);
        }

        public CoinsSpawnArea CreateCoinsSpawnArea(BoxCollider areaCollider)
        {
            _coinsSpawnArea = new CoinsSpawnArea(areaCollider, _spawner);
            _coinPicker.SetCoinArea(_coinsSpawnArea);
            return _coinsSpawnArea;
        }

        public Inventory CreateInventory()
        {
            return new Inventory();
        }

        private void SubscribeSceneLoadEvent()
        {
            _sceneLoader.SceneLoaded += HandleSceneLoad;
        }

        private void HandleSceneLoad()
        {
            _input = new Input();
            _ = SpawnPlayerAsync();
            _ = SpawnCoinsSpawnArea();

            SceneLoaded?.Invoke();
        }

        private async Task SpawnPlayerAsync()
        {
            var playerView = await _spawner.SpawnAsync(AssetName.Remy);
            await _spawner.SpawnAsync(AssetName.CameraWithCinemachineBrain);
            var cinemachineCamera = await _spawner.SpawnAsync(AssetName.FreeLookCinemachine);

            PlayerSpawned?.Invoke(playerView, cinemachineCamera);
        }

        private async Task SpawnCoinsSpawnArea()
        {
            var area = await _spawner.SpawnAsync(AssetName.CoinsSpawnArea);
            CoinsSpawnAreaSpawned?.Invoke(area);
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
