using Assets.Scripts.ThirdPersonGame.Controllers.DanceMinigame;
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

        private void HandlePlayerSpawnEvent(GameObject playerGameObject, GameObject camera)
        {
            var playerView = playerGameObject.GetComponent<PlayerView>();

            var animator = new Core.Animator(playerView.Animator, playerView.AnimatorStates.ToDictionary());

            var playerMover = _game.CreatePlayerMover(
                playerView.CharacterController,
                animator,
                playerView.transform,
                playerView.MovementVelocity,
                playerView.JumpForce,
                playerView.MovementAcceleration,
                playerView.MovementDeceleration);

            var cinechine = camera.GetComponent<CinemachineCamera>();
            cinechine.Target.TrackingTarget = playerView.CharacterController.transform;

            var playerRotator = _game.CreatePlayerRotator(playerView.CharacterController.transform, camera.transform,
                animator);

            new PlayerController(playerView, playerMover, playerRotator);

            var inventory = _game.CreateInventory();
            CreateInventoryController(inventory);

            var player = _game.CreatePlayer(playerMover, playerRotator, inventory, playerView.CharacterController, animator);
            player.BehaviourStateChanged += HandlePlayerBehaviourStateChange;
            player.DanceStarted += HandlePlayerDanceStart;
            player.DanceInterrupted += HandlePlayerDanceInterrupt;

            new DanceActionZoneController(player, _levelUIRoot.ActionZones);
            new DanceScoreController(player.DanceScore, _levelUIRoot.DanceScoreUI);
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

        private void HandlePlayerBehaviourStateChange(CharacterBehaviourState characterBehaviourState)
        {
            _levelUIRoot.ChangeStateView.SetState(characterBehaviourState);
        }

        private void HandlePlayerDanceStart(object sender, System.EventArgs e)
        {
            _levelUIRoot.ShowDanceMinigameHUD();
        }

        private void HandlePlayerDanceInterrupt(object sender, System.EventArgs e)
        {
            _levelUIRoot.ShowMainHUD();
        }
    }
}
