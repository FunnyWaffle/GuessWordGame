using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.GuessWordGame.UI
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Text _difficultySelectionText;
        [SerializeField] private List<UIDifficultySelectionButton> _difficultySelectionButtons;

        public IReadOnlyList<UIDifficultySelectionButton> UIDifficultySelectionButtons => _difficultySelectionButtons;
        public void SubscribeDifficultySelectionEvent(Game game)
        {
            foreach (var button in _difficultySelectionButtons)
            {
                button.ButtonClicked += game.ChooseDifficulty;
            }
        }
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
