using Assets.Scripts.ThirdPersonGame;
using Assets.Scripts.ThirdPersonGame.Data;
using UnityEditor;
using UnityEngine;

public class EditorSerializer : EditorWindow
{
    [MenuItem("My Tools/Save UI")]
    private static void SaveUI()
    {
        var uiRoot = GameObject.Find("UI");

        var uiData = new UIData();

        for (int i = 0; i < uiRoot.transform.childCount; i++)
        {
            var child = uiRoot.transform.GetChild(i);

            uiData.AddData(new UIElementData(child.name, child.parent.name));
        }

        _ = Storage.Save(uiData, Storage.UIDataPath);
    }
}
