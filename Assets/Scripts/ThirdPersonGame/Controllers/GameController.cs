using Assets.Scripts.ThirdPersonGame.Controllers.LevelSelection;
using Assets.Scripts.ThirdPersonGame.Core;
using Assets.Scripts.ThirdPersonGame.View;
using Assets.Scripts.ThirdPersonGame.View.UI.EntryPoint;
using Assets.Scripts.ThirdPersonGame.View.UI.EntryPoint.LevelSelection;
using Assets.Scripts.ThirdPersonGame.View.UI.Level;
using Unity.Cinemachine;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Controllers
{
    public class GameController
    {
        private readonly Game _game;

        private LevelUIRoot _levelUIRoot;

        public GameController(Game game, EntryPointUIRoot uIRoot)
        {
            _game = game;

            CreateEntryPointControllers(uIRoot);

            _game.SceneLoaded += HandleSceneLoad;
            _game.PlayerSpawned += HandlePlayerSpawnEvent;
            _game.CoinsSpawnAreaSpawned += HadleCoinsSpawnAreaSpawnEvent;
        }

        private void CreateEntryPointControllers(EntryPointUIRoot uIRoot)
        {
            CreateLevelSelectionMenuController(uIRoot.LevelSelectionMenu);
        }

        private void HandleSceneLoad()
        {
            _levelUIRoot = Object.FindFirstObjectByType<LevelUIRoot>();
        }

        private LevelSelectionMenuController CreateLevelSelectionMenuController(LevelSelectionMenu levelSelectionMenu) =>
           new(_game, levelSelectionMenu);

        private void HandlePlayerSpawnEvent(GameObject player, GameObject camera)
        {
            var playerView = player.GetComponent<PlayerView>();
            var playerMover = _game.CreatePlayerMover(
                playerView.CharacterController,
                playerView.Animator,
                playerView.transform,
                playerView.MovementVelocity,
                playerView.JumpForce,
                playerView.MovementAcceleration,
                playerView.MovementDeceleration);

            var cinechine = camera.GetComponent<CinemachineCamera>();
            cinechine.Target.TrackingTarget = playerView.CharacterController.transform;

            var playerRotator = _game.CreatePlayerRotator(playerView.CharacterController.transform, camera.transform);

            new PlayerController(playerView, playerMover, playerRotator);

            var inventory = _game.CreateInventory();
            CreateInventoryController(inventory);

            _game.CreatePlayer(playerMover, playerRotator, inventory, playerView.CharacterController);
        }

        private void CreateInventoryController(Inventory inventory)
        {
            new InventoryController(inventory, _levelUIRoot.InventoryView);
        }

        private void HadleCoinsSpawnAreaSpawnEvent(GameObject areaObject)
        {
            var areaView = areaObject.GetComponent<CoinsSpawnAreaView>();
            var area = _game.CreateCoinsSpawnArea(areaView.BoxCollider);
            new CoinsSpawnAreaController(area, areaView);
        }
    }
}
