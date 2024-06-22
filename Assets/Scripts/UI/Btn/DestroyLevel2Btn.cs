using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestroyLevel2Btn : EventTriggerEX
{
    private void Start()
    {
        init();
    }

    protected override void OnPointerDown(PointerEventData data)
    {
        MainController.main.sound.Play("Click");
        Time.timeScale = 1;
        DOTween.timeScale = 1;
        MainController.main.UI.DestroyUI(Define.UIlevel.Level2);
    }
}
