using System;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class LevelSelectionButton
    {
        public LevelSelectionButton(int levelId, string levelName)
        {
            LevelId = levelId;
            LevelName = levelName;
        }

        public int LevelId { get; }
        public string LevelName { get; }

        public event Action<int> ButtonClicked;

        public void HandleLevelSelection()
        {
            ButtonClicked?.Invoke(LevelId);
        }
    }
}
