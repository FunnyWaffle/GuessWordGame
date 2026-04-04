using Assets.Scripts.ThirdPersonGame.Core.Minigames;
using Assets.Scripts.ThirdPersonGame.View.UI.DanceMinigameHud;

namespace Assets.Scripts.ThirdPersonGame.Controllers.DanceMinigame
{
    public class DanceScoreController
    {
        private readonly DanceScore _danceScore;
        private readonly DanceScoreUI _danceScoreUI;

        public DanceScoreController(DanceScore danceScore, DanceScoreUI danceScoreUI)
        {
            _danceScore = danceScore;
            _danceScoreUI = danceScoreUI;

            _danceScore.ScoreChanged += HandleScoreChange;
        }

        private void HandleScoreChange(int value)
        {
            _danceScoreUI.SetScore(value);
        }
    }
}
