using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HomeBtn : EventTriggerEX
{
    void Start()
    {
        init();

        if (!(GameObject.Find("Level1").transform.GetChild(0).name == "Main(Clone)"))
            gameObject.SetActive(false);
    }

    protected override void OnPointerDown(PointerEventData data)
    {
        MainController.main.sound.Play("Click");
        CardManager.instance.GameReset();
        DataManager.Single.Data.inGameData.isMain = false;
        MainController.main.UI.UIsetting(Define.UIlevel.Level1, Define.UItype.Start);
        MainController.main.UI.DestroyUI(Define.UIlevel.Level2);
        MainController.main.UI.UIsetting(Define.UIlevel.Level3, Define.UItype.Loading);
    }
}