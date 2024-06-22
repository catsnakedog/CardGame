using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartYesBtn : EventTriggerEX
{
    void Start()
    {
        init();
    }

    protected override void OnPointerDown(PointerEventData data)
    {
        MainController.main.sound.Play("Click");
        DataManager.Single.Data.inGameData.isMain = true;
        DataManager.Single.Data.inGameData.isContinue = false;
        MainController.main.UI.UIsetting(Define.UIlevel.Level1, Define.UItype.Main);
        MainController.main.UI.DestroyUI(Define.UIlevel.Level2);
        MainController.main.UI.UIsetting(Define.UIlevel.Level3, Define.UItype.Loading);
    }
}