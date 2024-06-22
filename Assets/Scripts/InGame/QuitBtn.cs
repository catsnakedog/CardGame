using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuitBtn : EventTriggerEX
{
    void Start()
    {
        init();
    }

    protected override void OnPointerClick(PointerEventData data)
    {
        CardManager.instance.GameReset();
        DataManager.Single.Data.inGameData.isMain = false;
        MainController.main.UI.UIsetting(Define.UIlevel.Level1, Define.UItype.Start);
        MainController.main.UI.UIsetting(Define.UIlevel.Level3, Define.UItype.Loading);
    }
}
