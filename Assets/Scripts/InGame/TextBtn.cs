using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextBtn : EventTriggerEX
{
    void Start()
    {
        init();
    }

    protected override void OnPointerClick(PointerEventData data)
    {
        if (!GameObject.Find("SelectPanel") && !InGameManager.instance.isEnd)
        {
            InGameManager.instance.NextText();
        }
    }
}