using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.GuessWordGame.UI
{
    public class StatisticsMenu : MonoBehaviour
    {
        private readonly Dictionary<Difficulty, DifficultyStatisticsUI> _difficultyStatistics = new();
        [SerializeField] private Transform _difficultyStatisticsGrid;
        public bool IsActive => gameObject.activeSelf;
        public void Initialize()
        {
            CreateStatisticsUIElements();
        }
        public void HandleDifficultyStatisticsChange(Difficulty difficultyType, DifficultyStatistics difficultyStatistics)
        {
            var statistics = _difficultyStatistics[difficultyType];
            statistics.Setparameters(difficultyStatistics.WinsCount,
                difficultyStatistics.LosesCount,
                difficultyStatistics.WinRate);
        }
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
        private void CreateStatisticsUIElements()
        {
            var difficulties = Config.DifficultyConfigs;
            foreach (var (difficulty, _) in difficulties)
            {
                var gameObject = new GameObject("DifficultyStatistics");
                var text = gameObject.AddComponent<TextMeshProUGUI>();

                gameObject.transform.SetParent(_difficultyStatisticsGrid, false);

                var difficultyStatisticsUI =
                    new DifficultyStatisticsUI($"{difficulty}" + " difficulty: wins - {0}, loses - {1}, win rate - {2:0.##}", text);
                _difficultyStatistics[difficulty] = difficultyStatisticsUI;
            }
        }
    }
}
