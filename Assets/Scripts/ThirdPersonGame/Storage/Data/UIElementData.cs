using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Data
{
    [Serializable]
    public class UIElementData
    {
        public UIElementData(string name, string parent, List<Component> components)
        {
            Name = name;
            Parent = parent;
            Components = components;
        }

        public string Name { get; private set; }
        public string Parent { get; private set; }
        public List<Component> Components { get; private set; }
    }
}
