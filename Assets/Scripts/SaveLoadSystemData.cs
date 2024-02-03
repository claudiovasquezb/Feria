using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public static class SaveLoadSystemData
{
    public static void SaveData<T>(T data, string path, string fileName)
    {
        string fullPath = Application.persistentDataPath + "/" + path + "/";
        bool checkFolderExist = Directory.Exists(fullPath);
        if(checkFolderExist == false)
        {
            Directory.CreateDirectory(fullPath);
        }
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(fullPath + fileName + ".json", json);
        Debug.Log("Save data ok. " + fullPath);
    }
    public static T LoadData<T>(string path, string fileName)
    {
        string fullPath = Application.persistentDataPath + "/" + path + "/" + fileName + ".json";
        if (File.Exists(fullPath))
        {
            string textJson = File.ReadAllText(fullPath);
            var obj = JsonUtility.FromJson<T>(textJson);
            return obj;
        }
        else
        {
            Debug.Log("Not file found on load data");
            return default;
        }
    }
}
