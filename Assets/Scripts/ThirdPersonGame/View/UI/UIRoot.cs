using Assets.Scripts.ThirdPersonGame.View.UI.LevelSelection;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.View.UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private LevelSelectionMenu _levelSelectionMenu;

        public LevelSelectionMenu LevelSelectionMenu => _levelSelectionMenu;
        public MainMenu MainMenu => _mainMenu;

        public void Initialize()
        {
            _levelSelectionMenu.Initialize();
        }
    }
}
