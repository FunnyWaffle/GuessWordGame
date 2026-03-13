using TMPro;

namespace Assets.Scripts.GuessWordGame.UI
{
    public class DifficultyStatisticsUI
    {
        private readonly TMP_Text _text;
        private readonly string _template;

        public DifficultyStatisticsUI(string template, TMP_Text text)
        {
            _template = template;
            _text = text;
            _text.text = string.Format(_template, 0, 0, 0f);
        }

        public void Setparameters(params object[] parameters)
        {
            _text.text = string.Format(_template, parameters);
        }
    }
}
