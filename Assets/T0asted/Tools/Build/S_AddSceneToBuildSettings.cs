#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DebugPrinter;
using System.IO;
using Enum_Utility;

public class S_AddSceneToBuildSettings : MonoBehaviour
{ 
    // Add menu item named "Example Window" to the Window menu
    [MenuItem("Joshua's Tools/Build/Add all scenes to build settings")]
    public static void Run()
    {
        SetEditorBuildSettingsScenes();

        //Show existing window instance. If one doesn't exist, make one.
        //EditorWindow.GetWindow(typeof(S_AddSceneToBuildSettings));
    }

    public static void SetEditorBuildSettingsScenes()
    {
        //List<EditorBuildSettingsScene> scenes = new List<EditorBuildSettingsScene>();
        //
        //var fileInfo = new DirectoryInfo(Application.dataPath + "/Scenes").GetFiles();
        //foreach (var item in fileInfo)
        //{
        //    if (item.Extension == ".unity")
        //    {
        //        var itemName = item.Name.Replace(".unity", string.Empty);
        //
        //        string pathToScene = AssetDatabase.GetAssetPath(item.GetObjectData());
        //        EditorBuildSettingsScene scene = new EditorBuildSettingsScene(pathToScene, true);
        //    }
        //}
        //
        //scenes.Add(scene);
        //EditorBuildSettings.scenes = scenes.ToArray();
    }

}
#endif