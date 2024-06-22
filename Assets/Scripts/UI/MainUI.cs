using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    void Start()
    {
        InGameManager.instance.HpMpSetting();
        if(DataManager.Single.Data.inGameData.isContinue)
        {
            InGameManager.instance.StartStory(DataManager.Single.Data.inGameData.storyList[DataManager.Single.Data.inGameData.storyCnt], 0);
        }
        else
        {
            InGameManager.instance.FirstCard();
        }
    }
}