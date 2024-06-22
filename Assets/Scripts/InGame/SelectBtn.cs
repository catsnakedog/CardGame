using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectBtn : EventTriggerEX
{
    [SerializeField]
    int num;

    void Start()
    {
        init();
    }

    protected override void OnPointerDown(PointerEventData data)
    {
        MainController.main.sound.Play("Click");
        InGameManager.instance.SelectSelect(num);
    }
}
