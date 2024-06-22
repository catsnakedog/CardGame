using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[System.Serializable]
public class SaveDataClass
{
    public InGameData inGameData;
    public OptionData optionData;
    public SaveData saveData;
    public TextData textData;

    public SaveDataClass(InGameData inGameData, OptionData optionData, TextData textData, SaveData saveData)
    {
        this.inGameData = inGameData;
        this.optionData = optionData;
        this.textData = textData;
        this.saveData = saveData;
    }
    public SaveDataClass()
    {
        inGameData = new InGameData();
        optionData = new OptionData();
        textData = new TextData();
        saveData = new SaveData();
    }
}

#region InGameData
[System.Serializable]
public class InGameData
{
    public bool isContinue;

    public int hp;
    public int mp;
    public int storyCnt;

    public List<Item> myItemList;
    public List<int> storyList;

    public bool isMain;

    public InGameData(bool isContinue, int hp, int mp, List<Item> myItemList, List<int> storyList, int storyCnt, bool isMain)
    {
        this.isContinue = isContinue;
        this.hp = hp;
        this.mp = mp;
        this.myItemList = myItemList;
        this.storyList = storyList;
        this.storyCnt = storyCnt;
        this.isMain = isMain;
    }

    public InGameData()
    {
        isContinue = false;
        hp = 0;
        mp = 0;
        myItemList = new List<Item>();
        storyList = new List<int>();
        storyCnt = 0;
        isMain = false;
    }
}
#endregion

#region SaveData
[System.Serializable]
public class SaveData
{
    public int hp;
    public int mp;
    public int storyCnt;

    public List<Item> myItemList;
    public List<int> storyList;

    public string[] endingList;

    public SaveData(int hp, int mp, List<Item> myItemList, List<int> storyList, int storyCnt, string[] endingList)
    {
        this.hp = hp;
        this.mp = mp;
        this.myItemList = myItemList;
        this.storyList = storyList;
        this.storyCnt = storyCnt;
        this.endingList = endingList;
    }

    public SaveData()
    {
        hp = 0;
        mp = 0;
        myItemList = new List<Item>();
        storyList = new List<int>();
        storyCnt = 0;
        endingList = new string[7];
    }
}
#endregion

#region OptionData

[System.Serializable]
public class OptionData
{
    public float volumeBGM;
    public float volumeSFX;
    public bool muteBGM;
    public bool muteSFX;

    public OptionData( float volumeBGM, float volumeSFX, bool muteBGM, bool muteSFX)
    {
        this.volumeBGM = volumeBGM;
        this.volumeSFX = volumeSFX;
        this.muteBGM = muteBGM;
        this.muteSFX = muteSFX;
    }

    public OptionData()
    {
        volumeBGM = 0.5f;
        volumeSFX = 0.5f;
        muteBGM = false;
        muteSFX = false;
    }
}

#endregion

#region TextData
[System.Serializable]
public class TextData
{
    public List<TextInfo> textInfo;

    public TextData(List<TextInfo> textInfo)
    {
        this.textInfo = textInfo;
    }

    public TextData()
    {
        textInfo = new List<TextInfo>();
    }
}

[System.Serializable]
public class TextInfo
{
    public int Case;
    public int Branch;
    public bool IsBranchChange;
    public int BranchCount;
    public string Branch1;
    public string Branch2;
    public string Branch3;
    public string Target;
    public string Image;
    public bool End;
    public int EndIndex;
    public string EndName;
    public string Text;

    public TextInfo(int @case, int branch, bool isBranchChange, int branchCount, string branch1, string branch2, string branch3, string target, string image, bool end, int endIndex, string endName, string text)
    {
        Case = @case;
        Branch = branch;
        IsBranchChange = isBranchChange;
        BranchCount = branchCount;
        Branch1 = branch1;
        Branch2 = branch2;
        Branch3 = branch3;
        Target = target;
        Image = image;
        End = end;
        EndIndex = endIndex;
        EndName = endName;
        Text = text;
    }

    public TextInfo()
    {
        Case = 0;
        Branch = 0;
        IsBranchChange = false;
        BranchCount = 0;
        Branch1 = "";
        Branch2 = "";
        Branch3 = "";
        Target = "";
        Image = "";
        End = false;
        EndIndex = 0;
        EndName = "";
        Text = "";
    }
}
#endregion