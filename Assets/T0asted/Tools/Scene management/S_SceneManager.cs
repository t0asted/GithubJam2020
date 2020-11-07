using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneUtility;

public class S_UISceneManager : MonoBehaviour
{
    public static void OpenScene(string sceneName, bool add)
    {
        scenesUtils.Open(sceneName, add);
    }

    public static void OpenScene(Scenes_Level scene, bool add)
    {
        scenesUtils.Open(scene.ToString(), add);
    }

    public static void OpenScene(Scenes_UI scene, bool add)
    {
        scenesUtils.Open(scene.ToString(), add);
    }

    public static void CloseScene(string sceneName)
    {
        scenesUtils.Close(sceneName);
    }

    public static void CloseScene(Scenes_Level sceneName)
    {
        scenesUtils.Close(sceneName.ToString());
    }

    public static void CloseScene(Scenes_UI sceneName)
    {
        scenesUtils.Close(sceneName.ToString());
    }

}

namespace SceneUtility
{
    public class scenesUtils : MonoBehaviour
    {
        public static void Open(string scene, bool add)
        {
            if (scene != "")
            {
                var name = "S_" + scene.ToString();
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    if (SceneManager.GetSceneAt(i).name == name)
                    {
                        Debug.Log("Scene Allready Loaded.");
                        return;
                    }
                }
                SceneManager.LoadSceneAsync(name, add ? LoadSceneMode.Additive : LoadSceneMode.Single);
                return;
            }
        }

        public static void Close(string scene)
        {
            var name = "S_" + scene.Replace("S_", ""); ;
            SceneManager.UnloadSceneAsync(name);
        }
    }
}