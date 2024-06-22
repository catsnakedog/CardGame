using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    TextAsset textAsset;

    JsonManager jsonManager; // json에서 값을 읽어오거나 저장하는 JsonManager
    public SaveDataClass Data; // 데이터를 저장하는 형식인 SaveDataClass
    public static DataManager Single;

    void Awake()
    {
        Data = new SaveDataClass();
        jsonManager = new JsonManager();

        jsonManager.textAsset = textAsset;

        Single = this;

        Load();
        Save();

        GameObject.FindWithTag("MainController").GetComponent<MainController>().init();
    }
    public void Save() // saveData에 기록된 데이터들을 json에 저장한다
    {
        jsonManager.SaveJson(Data);
    }

    public void Load() // json에 기록돼있는 데이터들을 불러온다
    {
        Data = jsonManager.LoadSaveData();
    }
}