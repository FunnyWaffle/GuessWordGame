using Assets.Scripts.ThirdPersonGame.View.UI.DanceMinigameHud;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.View.UI.Level
{
    public class LevelUIRoot : MonoBehaviour
    {
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private ChangeStateView _changeStateView;
        [SerializeField] private DanceActionZoneUI[] _actionZones;
        [SerializeField] private DanceScoreUI _danceScoreUI;

        public InventoryView InventoryView => _inventoryView;
        public ChangeStateView ChangeStateView => _changeStateView;
        public DanceActionZoneUI[] ActionZones => _actionZones;
        public DanceScoreUI DanceScoreUI => _danceScoreUI;

        private void Start()
        {
            ShowMainHUD();
        }

        public void ShowMainHUD()
        {
            _inventoryView.gameObject.SetActive(true);

            _danceScoreUI.gameObject.SetActive(false);
            foreach (var zone in _actionZones)
            {
                zone.gameObject.SetActive(false);
            }
        }

        public void ShowDanceMinigameHUD()
        {
            _inventoryView.gameObject.SetActive(false);

            _danceScoreUI.gameObject.SetActive(true);
            foreach (var zone in _actionZones)
            {
                zone.gameObject.SetActive(true);
            }
        }
    }
}
