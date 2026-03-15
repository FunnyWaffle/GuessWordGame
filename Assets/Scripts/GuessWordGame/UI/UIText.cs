using TMPro;
using UnityEngine;

namespace Assets.Scripts.GuessWordGame.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class UIText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void SetWord(string word)
        {
            _text.text = word;
        }
    }
}
