using System;

namespace Assets.Scripts.ThirdPersonGame.Data
{
    [Serializable]
    public class UIElementData
    {
        public UIElementData(string name, string parent)
        {
            Name = name;
            Parent = parent;
        }

        public string Name { get; private set; }
        public string Parent { get; private set; }
    }
}
