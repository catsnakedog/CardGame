using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundController : MonoBehaviour
{
    AudioClip[] BGMs = new AudioClip[(int)Define.BGM.MaxCount];
    AudioClip[] SFXs = new AudioClip[(int)Define.SFX.MaxCount];

    HashSet<string> SFXNames = new HashSet<string>();
    HashSet<string> BGMNames = new HashSet<string>();

    public AudioSource BGMSource;
    public AudioSource SFXSource;

    public void init()
    {
        SoundPooling(); // 사운드 파일들을 풀링 해온다
        BGMSource = gameObject.AddComponent<AudioSource>();
        SFXSource = gameObject.AddComponent<AudioSource>();
        BGMSource.loop = true;
        BGMSource.volume = DataManager.Single.Data.optionData.volumeBGM;
        SFXSource.volume = DataManager.Single.Data.optionData.volumeSFX;
    }

    void SoundPooling() // enum에서 사운드 이름을 읽어와서 해당하는 사운드 파일을 로드 시킨다
    {
        string[] BGMNames = System.Enum.GetNames(typeof(Define.BGM));
        string[] SFXNames = System.Enum.GetNames(typeof(Define.SFX));
        for (int i = 0; i < BGMNames.Length - 1; i++)
        {
            BGMs[i] = Resources.Load<AudioClip>("Sound/BGM/" + BGMNames[i]);
            this.BGMNames.Add(BGMNames[i]);
        }
        for (int i = 0; i < SFXNames.Length - 1; i++)
        {
            SFXs[i] = Resources.Load<AudioClip>("Sound/SFX/" + SFXNames[i]);
            this.SFXNames.Add(SFXNames[i]);
        }
    }

    public void SoundSetting()
    {
        BGMSource.volume = DataManager.Single.Data.optionData.volumeBGM;
        SFXSource.volume = DataManager.Single.Data.optionData.volumeSFX;
    }

    public void Play(string soundName, float pitch = 1.0f) // 전달받은 soundName을 찾아서 실행시킨다
    {
        BGMSource.volume = DataManager.Single.Data.optionData.volumeBGM;
        SFXSource.volume = DataManager.Single.Data.optionData.volumeSFX;
        if (BGMNames.Contains(soundName))
        {
            if (BGMSource.isPlaying)
            {
                BGMSource.Stop();
            }
            BGMSource.pitch = pitch;
            BGMSource.clip = BGMs[(int)((Define.BGM)Enum.Parse(typeof(Define.BGM), soundName))];
            BGMSource.Play();
        }
        else if (SFXNames.Contains(soundName))
        {
            SFXSource.pitch = pitch;
            SFXSource.PlayOneShot(SFXs[(int)((Define.SFX)Enum.Parse(typeof(Define.SFX), soundName))]);
        }
        else
        {
            Debug.Log("해당 사운드는 존재하지 않습니다");
        }
    }
}