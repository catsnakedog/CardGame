using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPrefab : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1;
        DOTween.timeScale = 1;
    }
}
