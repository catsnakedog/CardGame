using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReGameBtn : EventTriggerEX
{
    void Start()
    {
        init();
    }

    protected override void OnPointerClick(PointerEventData data)
    {
        if(CardManager.instance.isFirstCard)
        {
            return;
        }
        MainController.main.sound.Play("Click");
        InGameManager.instance.GameReset();
    }
}