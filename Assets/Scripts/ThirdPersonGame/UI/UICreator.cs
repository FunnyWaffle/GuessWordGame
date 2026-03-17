using Assets.Scripts.ThirdPersonGame.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.UI
{
    public class UICreator
    {
        public UICreator()
        {
            _ = Create();
        }
        private async Task Create()
        {
            var datas = await Storage.Load(new UIData(), Storage.UIDataPath);

            var createdObjects = new List<GameObject>();
            foreach (var data in datas.Datas)
            {
                var parent = createdObjects.FirstOrDefault(gameObject => gameObject.name == data.Parent);

                var uiElement = new GameObject(data.Name);

                if (parent != null)
                    uiElement.transform.SetParent(parent.transform);

                createdObjects.Add(uiElement);
            }
        }
    }
}
