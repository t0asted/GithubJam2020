#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

public class S_CreateDirectory : MonoBehaviour
{

    [MenuItem("Joshua's Tools/Directorys/Streaming Assets")]
    public static void AddStreamingAssets()
    {
        if (!File.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
    }

    [MenuItem("Joshua's Tools/Directorys/Persistant Data Path")]
    public static void AddPersistantDataPath()
    {
        if (!File.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.persistentDataPath);
        }
        
    }

    [MenuItem("Joshua's Tools/Directorys/Persistant Data")]
    public static void AddData()
    {
        if (!File.Exists(Application.persistentDataPath + "/Data/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Data/");
        }
    }

    [MenuItem("Joshua's Tools/Directorys/Settings File")]
    public static void AddSettingsFile()
    {
        if (!File.Exists(Application.persistentDataPath + "/Data/GameSettings.txt"))
        {
            FileStream file = File.Create(Application.persistentDataPath + "/Data/GameSettings.txt");
            file.Close();
        }
    }
}
#endif