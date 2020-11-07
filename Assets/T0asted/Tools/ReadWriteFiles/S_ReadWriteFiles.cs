using System.IO;
using UnityEngine;

namespace ReadWrite_Utility
{
    public class ReadWriteUtility : MonoBehaviour
    {
        private static string m_readString;
        private static string m_dataPath = Application.persistentDataPath + "/Data/";
        private static string m_settingsFile = "GameSettings.txt";
        private static string m_playerFile = "PlayerData.txt";
        private static string m_LevelsFile = "LevelsData.txt";

        public static void WriteString(string changePath, string fileName, object classToWrite)
        {
            CheckFile(changePath + "/" + fileName);
            StreamWriter writer = new StreamWriter(changePath + "/" + fileName, false);
            writer.WriteLine(JsonString(classToWrite));
            writer.Close();
        }

        public static void WriteString(string fileName, object classToWrite)
        {
            CheckFile(m_dataPath + "/" + fileName);
            StreamWriter writer = new StreamWriter(m_dataPath + "/" + fileName, false);
            writer.WriteLine(JsonString(classToWrite));
            writer.Close();
        }

        public static void WriteString(FilesToEdit fileType, object classToWrite)
        {
            var StringFileToWrite = FileName(fileType);
            CheckFile(m_dataPath + "/" + StringFileToWrite);
            StreamWriter writer = new StreamWriter(m_dataPath + "/" + StringFileToWrite, false);
            writer.WriteLine(JsonString(classToWrite));
            writer.Close();
        }

        public static string ReadString(string fileName)
        {
            CheckFile(m_dataPath + fileName);
            StreamReader reader = new StreamReader(m_dataPath + fileName);
            m_readString = reader.ReadToEnd();
            reader.Close();
            return m_readString;
        }

        public static string ReadString(FilesToEdit fileType)
        {
            var StringFileToRead = FileName(fileType);

            CheckFile(m_dataPath + StringFileToRead);
            StreamReader reader = new StreamReader(m_dataPath + StringFileToRead);
            m_readString = reader.ReadToEnd();
            reader.Close();
            return m_readString;
        }

        private static string FileName(FilesToEdit fileType)
        {
            var StringFileToRead = "";

            switch (fileType)
            {
                case FilesToEdit.Settings:
                    StringFileToRead = m_settingsFile;
                    break;
                case FilesToEdit.Player:
                    StringFileToRead = m_playerFile;
                    break;
                case FilesToEdit.Levels:
                    StringFileToRead = m_LevelsFile;
                    break;
            }
            return StringFileToRead;
        }

        private static string JsonString(object objectToConvert)
        {
            return JsonUtility.ToJson(objectToConvert);
        }

        private static void CheckFile(string path)
        {
            if (!File.Exists(m_dataPath))
            {
                Directory.CreateDirectory(m_dataPath);
            }

            if (!File.Exists(path))
            {
                FileStream file = File.Create(path);
                file.Close();
            }
        }

    }
}

public enum FilesToEdit
{
    Settings,
    Player,
    Levels
}