using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GuessWordGame.UI
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private TMP_Text _volumeValue;
        private void Start()
        {
            _volumeSlider.value = 50f;
        }
        private void OnEnable()
        {
            _volumeSlider.onValueChanged.AddListener(HandleVolumeValueChange);
        }
        private void OnDisable()
        {
            _volumeSlider.onValueChanged.RemoveListener(HandleVolumeValueChange);
        }
        public void SetActive(bool isActive) => gameObject.SetActive(isActive);
        private void HandleVolumeValueChange(float value)
        {
            _volumeValue.text = value.ToString("P0");
        }
    }
}
