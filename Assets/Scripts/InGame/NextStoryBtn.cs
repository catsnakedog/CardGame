using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextStoryBtn : EventTriggerEX
{
    void Start()
    {
        init();
    }

    protected override void OnPointerClick(PointerEventData data)
    {
        MainController.main.sound.Play("CardNext");
        gameObject.SetActive(false);
        InGameManager.instance.StartStory(DataManager.Single.Data.inGameData.storyList[DataManager.Single.Data.inGameData.storyCnt], 0);
        InGameManager.instance.isEnd = false;
    }
}
