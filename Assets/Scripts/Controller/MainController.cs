using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public static MainController main;

    public UILevelController UI { get; set; }
    public ResourceController resource { get; set; }
    public SoundController sound { get; set; }

    public void init()
    {
        main = this.GetComponent<MainController>();
        resource = gameObject.AddComponent<ResourceController>();
        UI = gameObject.AddComponent<UILevelController>();
        sound = gameObject.AddComponent<SoundController>();
        resource.init();
        UI.init();
        sound.init();
    }
}