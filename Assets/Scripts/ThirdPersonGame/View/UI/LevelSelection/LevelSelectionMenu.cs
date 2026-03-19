using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ThirdPersonGame.View.UI.LevelSelection
{
    public class LevelSelectionMenu : MonoBehaviour
    {
        [SerializeField] private LevelSelectionButtonView _buttonPrefab;
        [SerializeField] private GridLayoutGroup _buttonsGrid;

        private Transform _buttonsGridTransform;
        private readonly List<LevelSelectionButtonView> _levelSelectionButtons = new();

        public void Initialize() => _buttonsGridTransform = _buttonsGrid.transform;

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public LevelSelectionButtonView CreateLevelSelectionButtonView()
        {
            var button = Instantiate(_buttonPrefab, _buttonsGridTransform);
            _levelSelectionButtons.Add(button);
            return button;
        }
    }
}
