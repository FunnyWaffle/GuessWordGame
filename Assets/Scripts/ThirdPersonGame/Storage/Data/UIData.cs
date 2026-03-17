using System;
using System.Collections.Generic;

namespace Assets.Scripts.ThirdPersonGame.Data
{
    [Serializable]
    public class UIData
    {
        public List<UIElementData> Datas { get; set; } = new();

        public void AddData(UIElementData data)
        {
            Datas.Add(data);
        }
    }
}
