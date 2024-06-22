using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class JsonManager
{
    [SerializeField]
    public TextAsset textAsset;
    public void SaveJson(SaveDataClass saveData) // 데이터를 저장하는 함수
    {
        string jsonText;
        string savePath = Application.dataPath + "/Data/GameData.json";

#if UNITY_EDITOR_WIN

#endif
#if UNITY_ANDROID
        savePath = Application.persistentDataPath + "/GameData.json";
#endif
        jsonText = JsonUtility.ToJson(saveData, true);
        FileStream fileStream = new FileStream(savePath, FileMode.Create);
        byte[] bytes = Encoding.UTF8.GetBytes(jsonText);
        fileStream.Write(bytes, 0, bytes.Length);
        fileStream.Close();
    }

    public SaveDataClass LoadSaveData()
    {
        SaveDataClass gameData;

        string loadPath = Application.dataPath + "/Data/GameData.json";

#if UNITY_EDITOR_WIN

#endif
#if UNITY_ANDROID
        loadPath = Application.persistentDataPath + "/GameData.json";
#endif
        if (!Directory.Exists(loadPath))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Data");
        }

        if (File.Exists(loadPath))
        {
            FileStream stream = new FileStream(loadPath, FileMode.Open);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Close();
            string jsonData = Encoding.UTF8.GetString(bytes);
            gameData = JsonUtility.FromJson<SaveDataClass>(jsonData);
        }
        else
        {
            gameData = JsonUtility.FromJson<SaveDataClass>(textAsset.ToString());
            SaveJson(gameData);
        }
        return gameData;
    }
}