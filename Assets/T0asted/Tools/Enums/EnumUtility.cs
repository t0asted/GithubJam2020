#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

namespace Enum_Utility
{
    public class EnumUtility : MonoBehaviour
    {
        public static string enumPath = "Assets/Scripts/Data/Enums/";

        //Scenes

        public static DirectoryInfo[] GetDirectorysFromPath(string path)
        {
            var info = new DirectoryInfo(path);
            var fileInfo = info.GetDirectories();

            if (fileInfo.Length == 0)
                return new DirectoryInfo[] { info };
                
            return fileInfo;
        }

        public static string[] GetNamesFromDirectory(DirectoryInfo path, string extention)
        {
            List<string> sceneNames = new List<string>();

            var fileInfo = path.GetFiles();
            foreach (var item in fileInfo)
            {
                if (item.Extension == extention)
                {
                    var itemName = item.Name.Replace(extention, string.Empty);
                    itemName = itemName.Replace("S_", string.Empty);

                    sceneNames.Add(itemName);
                }
            }

            return sceneNames.ToArray();
        }

    }
}
#endif