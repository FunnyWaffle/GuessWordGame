using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

namespace Assets.Scripts.ThirdPersonGame.View.UI.EntryPoint.LevelSelection
{
    [RequireComponent(typeof(Button))]
    public class LevelSelectionButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;

        public ButtonClickedEvent ButtonClickedEvent => _button.onClick;

        public void SetText(string value) =>
            _text.SetText(value);

        private void OnValidate()
        {
            _button = gameObject.GetComponent<Button>();
            _text = gameObject.GetComponentInChildren<TMP_Text>();
        }
    }
}
