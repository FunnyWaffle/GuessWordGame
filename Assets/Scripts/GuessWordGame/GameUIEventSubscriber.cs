using Assets.Scripts.GuessWordGame.UI;
using System.Threading.Tasks;

namespace Assets.Scripts.GuessWordGame
{
    public class GameUIEventSubscriber
    {
        public GameUIEventSubscriber(Game game, GameUI gameUI)
        {
            gameUI.SubscribeGameOnUIEvents(game);
            gameUI.MainMenu.RestartButton.onClick.AddListener(game.RestartGame);

            game.GameStarted += gameUI.HandleGameStart;
            game.GameEnded += gameUI.HandleGameEnd;

            game.WordMaskChanged += gameUI.GameplayMenu.HandleMaskChange;
            game.AttemptsCountChanged += gameUI.GameplayMenu.HandleAttemptsCountChange;
            game.GuessedWordsCountChanged += gameUI.GameplayMenu.HandleGuessedWordsCountChange;
            game.CurrentLifeCountChanged += gameUI.GameplayMenu.HandleCurrentLifeCountChange;
            game.MaxLifeCountChanged += gameUI.GameplayMenu.HandleMaxLifeCountChange;
            game.WordGenerated += gameUI.GameplayMenu.ClearButtonColors;

            game.LettersBank.GuessedLettersChanged += gameUI.GameplayMenu.HandleGuessedLettersChange;
            game.LettersBank.FailedLettersChanged += gameUI.GameplayMenu.HandleFailedLettersChange;

            game.Statistics.DifficultyStatisticsChanged += gameUI.StatisticsMenu.HandleDifficultyStatisticsChange;

            _ = SubscribeLetterButtonsClickEvent(game, gameUI);
        }
        private async Task SubscribeLetterButtonsClickEvent(Game game, GameUI gameUI)
        {
            await gameUI.GameplayMenu.ButtonsInitializeTask;

            foreach (var button in gameUI.GameplayMenu.LetterButtons)
            {
                button.ButtonClicked += game.HandleInput;
            }
        }
    }
}
