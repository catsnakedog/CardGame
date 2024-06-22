using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum UIlevel
    {
        Level1,
        Level2,
        Level3,
        MaxCount 
    }

    public enum UItype
    {
        Start,
        LeftPopUp,
        CreditPopUp,
        ExtraPopUp,
        ContinuePopUp,
        StartPopUp,
        OptionPopUp,
        Main,
        MainUI,
        Loading,
        MaxCount
    }

    public enum SpriteDict
    {
        bath,
        fogforest,
        fond,
        kiwa,
        lightforest,
        nori,
        rudolf,
        wingouter,
        MaxCount
    }

    public enum BGM
    {
        MaxCount
    }

    public enum SFX
    {
        Bat,
        CardGet,
        CardNext,
        Click,
        FootSound,
        Rope,
        Tiger,
        Water,
        MaxCount
    }
}