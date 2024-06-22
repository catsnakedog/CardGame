using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreditBtn : EventTriggerEX
{
    void Start()
    {
        init();
    }

    protected override void OnPointerDown(PointerEventData data)
    {
        MainController.main.sound.Play("Click");
        MainController.main.UI.UIsetting(Define.UIlevel.Level2, Define.UItype.CreditPopUp);
    }
}