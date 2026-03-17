using Assets.Scripts.ThirdPersonGame;
using Assets.Scripts.ThirdPersonGame.Data;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class EditorSerializer : EditorWindow
{
    [MenuItem("My Tools/Save UI")]
    private static async Task SaveUI()
    {
        var uIRoot = GameObject.Find("UI");

        var uIData = new UIData();
        uIData.Datas.Add(new UIElementData(uIRoot.name, null, uIRoot.GetComponents<Component>().ToList()));

        GetChildUIElementData(uIRoot, uIData);

        try
        {
            var serializeSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            await Storage.Save(uIData, Storage.UIDataPath, serializeSettings);
        }
        catch (System.Exception e)
        {
            throw e;
        }
    }
    private static void GetChildUIElementData(GameObject uIElement, UIData uIData)
    {
        var uIElementTransform = uIElement.transform;
        for (int i = 0; i < uIElementTransform.childCount; i++)
        {
            var child = uIElementTransform.GetChild(i);
            uIData.AddData(new UIElementData(child.name, child.parent.name, child.GetComponents<Component>().ToList()));

            GetChildUIElementData(child.gameObject, uIData);
        }
    }
}
