using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    TextAsset textAsset;

    JsonManager jsonManager; // json���� ���� �о���ų� �����ϴ� JsonManager
    public SaveDataClass Data; // �����͸� �����ϴ� ������ SaveDataClass
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
    public void Save() // saveData�� ��ϵ� �����͵��� json�� �����Ѵ�
    {
        jsonManager.SaveJson(Data);
    }

    public void Load() // json�� ��ϵ��ִ� �����͵��� �ҷ��´�
    {
        Data = jsonManager.LoadSaveData();
    }
}