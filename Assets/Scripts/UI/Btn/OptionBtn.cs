using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionBtn : EventTriggerEX
{
    void Start()
    {
        init();
    }

    protected override void OnPointerDown(PointerEventData data)
    {
        MainController.main.sound.Play("Click");
        Time.timeScale = 0;
        DOTween.timeScale = 0;
        MainController.main.UI.UIsetting(Define.UIlevel.Level2, Define.UItype.OptionPopUp);
    }
}
