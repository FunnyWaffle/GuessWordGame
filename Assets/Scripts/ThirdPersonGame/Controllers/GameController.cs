using Assets.Scripts.ThirdPersonGame.Controllers.LevelSelection;
using Assets.Scripts.ThirdPersonGame.Core;
using Assets.Scripts.ThirdPersonGame.View;
using Assets.Scripts.ThirdPersonGame.View.UI;
using Assets.Scripts.ThirdPersonGame.View.UI.LevelSelection;
using Unity.Cinemachine;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Controllers
{
    public class GameController
    {
        private readonly Game _game;
        private readonly UIRoot _uIRoot;

        public GameController(Game game, UIRoot uIRoot)
        {
            _game = game;
            _uIRoot = uIRoot;

            CreateControllers();

            _game.PlayerSpawned += CreatePlayerController;
            _game.CoinsSpawnAreaSpawned += HadleCoinsSpawnAreaSpawnEvent;
        }

        public void CreateControllers()
        {
            CreateLevelSelectionMenuController(_uIRoot.LevelSelectionMenu);
        }

        private LevelSelectionMenuController CreateLevelSelectionMenuController(LevelSelectionMenu levelSelectionMenu) =>
           new(_game, levelSelectionMenu);

        private void CreatePlayerController(GameObject player, GameObject camera)
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
        }

        private void HadleCoinsSpawnAreaSpawnEvent(GameObject areaObject)
        {
            var areaView = areaObject.GetComponent<CoinsSpawnAreaView>();
            var area = _game.CreateCoinsSpawnArea(areaView.BoxCollider);
            new CoinsSpawnAreaController(area, areaView);
        }
    }
}
