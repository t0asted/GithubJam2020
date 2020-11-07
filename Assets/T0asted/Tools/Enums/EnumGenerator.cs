#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using UnityEngine;
using Enum_Utility;

[ExecuteInEditMode]
public class EnumGenerator : MonoBehaviour
{
    [MenuItem("Joshua's Tools/Enums/Generate All")]
    public static void GenerateAll()
    {
        GenerateScenes();
        GenerateItems();
    }

    [MenuItem("Joshua's Tools/Enums/Clear All")]
    public static void ClearAll()
    {
        ClearScenes();
        ClearSuckableItems();
    }

    // Generate enums.

    [MenuItem("Joshua's Tools/Enums/Generate Scenes")]
    public static void GenerateScenes()
    {
        Generate("Scenes", "Assets/Scenes/", ".unity", "S_", true);
    }

    [MenuItem("Joshua's Tools/Enums/Generate Items")]
    public static void GenerateItems()
    {
        Generate("Item", "Assets/Data/Items/", ".asset");
    }

    // Clear enums.

    [MenuItem("Joshua's Tools/Enums/Clear Scenes")]
    public static void ClearScenes()
    {
        Clear("Scenes", "Assets/Scenes/");
    }

    [MenuItem("Joshua's Tools/Enums/Clear Items")]
    public static void ClearSuckableItems()
    {
        Clear("Suckable", "Assets/Prefabs/Items/");
    }

    // Generate enums

    private static void Generate(string Generating, string path, string extention, string prefixToDelete = "", bool serialiable = false)
    {
        DoesDirectoryExsist(GetEnumPath(Generating));

        var Directorys = EnumUtility.GetDirectorysFromPath(path);

        foreach (var item in Directorys)
        {
            string[] enumEntries = EnumUtility.GetNamesFromDirectory(item, extention);

            DoesFileExsist(GetEnumScriptPathName(Generating, item.Name));

            using (StreamWriter streamWriter = new StreamWriter(GetEnumScriptPathName(Generating, item.Name)))
            {
                if (serialiable)
                {
                    streamWriter.WriteLine("[System.Serializable]");
                }
                streamWriter.WriteLine("public enum " + GetEnumName(Generating, item.Name));
                streamWriter.WriteLine("{");
                streamWriter.WriteLine("    None = 0");

                for (int i = 0; i < enumEntries.Length; i++)
                {
                    streamWriter.WriteLine("\t," + ((prefixToDelete != "") ? enumEntries[i].Replace(prefixToDelete, "") : enumEntries[i]) + " = " + enumEntries[i].GetHashCode().ToString());
                }
                streamWriter.WriteLine("}");
            }
            AssetDatabase.Refresh();
        }
    }

    private static void Clear(string clearing, string path)
    {
        DoesDirectoryExsist(GetEnumPath(clearing));

        var Directorys = EnumUtility.GetDirectorysFromPath(path);
        foreach (var item in Directorys)
        {
            DoesFileExsist(GetEnumScriptPathName(clearing, item.Name));

            using (StreamWriter streamWriter = new StreamWriter(GetEnumScriptPathName(clearing, item.Name)))
            {
                streamWriter.WriteLine("public enum " + GetEnumName(clearing, item.Name));
                streamWriter.WriteLine("{");
                streamWriter.WriteLine("    None = 0");
                streamWriter.WriteLine("}");
            }
            AssetDatabase.Refresh();
        }
    }


    // Check if Path/File exsist if not create them.

    private static void DoesDirectoryExsist(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    private static void DoesFileExsist(string file)
    {
        if (!File.Exists(file))
        {
            var newFile = File.Create(file);
            newFile.Close();
        }
    }

    private static string GetEnumScriptPathName(string typePass, string enumNamePass)
    {
        return GetEnumPath(typePass) + GetEnmumScriptName(typePass, enumNamePass);
    }

    private static string GetEnumPath(string TypePass)
    {
        return EnumUtility.enumPath + TypePass + "/";
    }

    private static string GetEnmumScriptName(string typePass, string itemNamePass)
    {
        return "Enum_" + GetEnumName(typePass, itemNamePass) + ".cs";
    }

    private static string GetEnumName(string typePass, string itemNamePass)
    {
        return typePass + "_" + itemNamePass;
    }
}
#endif