using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    [SerializeField]
    Slider BGM;
    [SerializeField]
    Slider SFX;

    public void Start()
    {
        BGM.value = DataManager.Single.Data.optionData.volumeBGM;
        SFX.value = DataManager.Single.Data.optionData.volumeSFX;
    }

    public void BGMChange()
    {
        DataManager.Single.Data.optionData.volumeBGM = BGM.value;
        MainController.main.sound.SoundSetting();
    }

    public void SFXChange()
    {
        DataManager.Single.Data.optionData.volumeSFX = SFX.value;
        MainController.main.sound.SoundSetting();
    }
}
