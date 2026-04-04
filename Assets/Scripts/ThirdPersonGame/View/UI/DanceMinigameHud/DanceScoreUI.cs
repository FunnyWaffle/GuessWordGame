using TMPro;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.View.UI.DanceMinigameHud
{
    public class DanceScoreUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _score;

        public void SetScore(int value)
        {
            _score.SetText(value.ToString());
        }
    }
}
