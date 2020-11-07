﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections;
using UnityEditor.SceneManagement;

[ExecuteInEditMode]
public class S_BootFromScene : EditorWindow
{
    [SerializeField] string lastScene = "";
    [SerializeField] int targetScene = 0;
    [SerializeField] string waitScene = null;
    [SerializeField] bool hasPlayed = false;

    //[MenuItem("Joshua's Tools/Boot From Scene")]
    public static void Run()
    {
        EditorWindow.GetWindow<S_BootFromScene>();
    }
    static string[] sceneNames;
    static EditorBuildSettingsScene[] scenes;

    void OnEnable()
    {
        scenes = EditorBuildSettings.scenes;
        sceneNames = scenes.Select(x => AsSpacedCamelCase(Path.GetFileNameWithoutExtension(x.path))).ToArray();
    }

    void Update()
    {
        if (!EditorApplication.isPlaying)
        {
            if (null == waitScene && !string.IsNullOrEmpty(lastScene))
            {
                //EditorApplication.OpenScene(lastScene);
                EditorSceneManager.OpenScene(lastScene);
                lastScene = null;
            }
        }
    }

    void OnGUI()
    {
        if (EditorApplication.isPlaying)
        {
            if (EditorApplication.currentScene == waitScene)
            {
                waitScene = null;
            }
            return;
        }

        if (EditorApplication.currentScene == waitScene)
        {
            EditorApplication.isPlaying = true;
        }
        if (null == sceneNames) return;
        targetScene = EditorGUILayout.Popup(targetScene, sceneNames);
        if (GUILayout.Button("Play"))
        {
            lastScene = EditorApplication.currentScene;
            waitScene = scenes[targetScene].path;
            EditorApplication.SaveCurrentSceneIfUserWantsTo();
            EditorApplication.OpenScene(waitScene);
        }
    }

    public string AsSpacedCamelCase(string text)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder(text.Length * 2);
        sb.Append(char.ToUpper(text[0]));
        for (int i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                sb.Append(' ');
            sb.Append(text[i]);
        }
        return sb.ToString();
    }
}
#endif