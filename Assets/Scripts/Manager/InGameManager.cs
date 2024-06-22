using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InGameManager : MonoBehaviour
{
    [SerializeField]
    List<int> table0;
    [SerializeField]
    List<int> table1;
    [SerializeField]
    List<int> table2;
    [SerializeField]
    List<int> table3;
    [SerializeField]
    List<int> table4;

    [SerializeField]
    GameObject textBox;
    [SerializeField]
    GameObject text;
    [SerializeField]
    GameObject blinkText;
    [SerializeField]
    GameObject startText;

    [SerializeField]
    List<string> itemName;

    [SerializeField]
    public List<TextInfo> textInfo;

    [SerializeField]
    public int cnt;
    [SerializeField]
    public int branch;

    [SerializeField]
    List<GameObject> texts;

    [SerializeField]
    GameObject blink;


    [SerializeField]
    GameObject next;

    [SerializeField]
    GameObject quit;

    [SerializeField]
    GameObject reGame;

    [SerializeField]
    List<GameObject> select;

    [SerializeField]
    GameObject BGImage;

    [SerializeField]
    ItemSO itemSO;

    bool isTextEnd;
    bool isStoryEnd;

    public static InGameManager instance;
    void Awake() => instance = this;

    void Start()
    {
        select = new List<GameObject>();
        cnt = 0;
    }

    public void GameReset()
    {
        DOTween.KillAll();
        DOTween.Clear();
        StopAllCoroutines();

        DataManager.Single.Data.inGameData.isContinue = false;
        DataManager.Single.Data.inGameData.hp = 3;
        DataManager.Single.Data.inGameData.mp = 3;
        DataManager.Single.Data.inGameData.storyCnt = 0;
        DataManager.Single.Data.inGameData.myItemList.Clear();
        DataManager.Single.Data.inGameData.storyList = StoryMake();

        DataManager.Single.Data.saveData.hp = 3;
        DataManager.Single.Data.saveData.mp = 3;
        DataManager.Single.Data.saveData.storyCnt = 0;
        DataManager.Single.Data.saveData.myItemList.Clear();
        DataManager.Single.Data.saveData.storyList.Clear();

        CardManager.instance.GameReset();

        cnt = 0;
        branch = 0;
        foreach(GameObject obj in texts)
        {
            Destroy(obj);
        }
        texts.Clear();

        if(blink != null)
        {
            Destroy(blink);
        }

        next.SetActive(false);
        foreach(GameObject obj in select)
        {
            obj.transform.GetChild(0).gameObject.SetActive(false);
            obj.SetActive(false);
        }

        isGameOver = false;
        isTextEnd = false;
        isStoryEnd = false;
        panel.SetActive(false);
        useCardPanel.SetActive(false);
        reGame.SetActive(false);
        quit.SetActive(false);
        lastHp = 0;
        lastMp = 0;
        lastImage = null;
        isEnd = false;
        isSelect = false;

        HpMpSetting();

        CardManager.instance.FindAndAddCard("빈 손");
        FirstCard();
    }


    public void FirstCard()
    {
        CardManager.instance.isFirstCard = true;
        CardManager.instance.SpawnRope(new List<Item> { itemSO.items[0], itemSO.items[UnityEngine.Random.Range(0, 9)], itemSO.items[UnityEngine.Random.Range(0, 9)] }, 0.2f);
    }

    public void GameStart()
    {
        isGameOver = false;
        textBox = GameObject.Find("TextBox");
        next = GameObject.Find("Next");
        reGame = GameObject.Find("ReGame");
        quit = GameObject.Find("Quit");
        select = new List<GameObject>();
        panel = GameObject.Find("SelectPanel");
        BGImage = GameObject.Find("BGImage2");
        useCardPanel = GameObject.Find("UseCardPanel");
        CardManager.instance.costLow = GameObject.Find("MpLowPanel");
        select.Add(GameObject.Find("Select").transform.GetChild(0).gameObject);
        select.Add(GameObject.Find("Select").transform.GetChild(1).gameObject);
        select.Add(GameObject.Find("Select").transform.GetChild(2).gameObject);
        CardManager.instance.RopePanel = GameObject.Find("RopePanel");
        next.SetActive(false);
        panel.SetActive(false);
        useCardPanel.SetActive(false);
        CardManager.instance.costLow.SetActive(false);
        CardManager.instance.RopePanel.SetActive(false);
        reGame.SetActive(false);
        quit.SetActive(false);
        CardManager.instance.isUseCard = false;

        isTextEnd = false;
        isStoryEnd = false;
        isEnd = false;
        isSelect = false;

        DataManager.Single.Data.inGameData.storyList = StoryMake();
        DataManager.Single.Data.inGameData.storyCnt = 0;
        DataManager.Single.Data.inGameData.hp = 3;
        DataManager.Single.Data.inGameData.mp = 3;
        DataManager.Single.Data.inGameData.myItemList = new List<Item>();

        if (DataManager.Single.Data.inGameData.isContinue)
        {
            if (DataManager.Single.Data.saveData.storyCnt == 0)
            {
                return;
            }
            DataManager.Single.Data.inGameData.storyList = DataManager.Single.Data.saveData.storyList;
            DataManager.Single.Data.inGameData.hp = DataManager.Single.Data.saveData.hp;
            DataManager.Single.Data.inGameData.mp = DataManager.Single.Data.saveData.mp;
            DataManager.Single.Data.inGameData.storyCnt = DataManager.Single.Data.saveData.storyCnt;
            DataManager.Single.Data.inGameData.myItemList = DataManager.Single.Data.saveData.myItemList;
            CardManager.instance.isFirstCard = false;
        }

        lastHp = 0;
        lastMp = 0;

        CardManager.instance.FindAndAddCard("빈 손");
    }

    string target;
    Coroutine textShow;

    public void StartStory(int Case, int Branch)
    {
        isStoryEnd = false;
        foreach (GameObject obj in texts)
        {
            Destroy(obj);
        }
        texts.Clear();
        textInfo = GetTextInfo(Case, Branch);
        cnt = 0;

        isTextEnd = false;
        textShow = StartCoroutine(TextShow(textInfo[cnt]));
    }

    public bool isEnd;

    public void EndStory()
    {
        branch = 0;
        DataManager.Single.Data.inGameData.storyCnt++;
        next.SetActive(true);
        DataManager.Single.Data.saveData.hp = DataManager.Single.Data.inGameData.hp;
        DataManager.Single.Data.saveData.mp = DataManager.Single.Data.inGameData.mp;
        DataManager.Single.Data.saveData.storyCnt = DataManager.Single.Data.inGameData.storyCnt;
        DataManager.Single.Data.saveData.myItemList = DataManager.Single.Data.inGameData.myItemList;
        DataManager.Single.Data.saveData.storyList = DataManager.Single.Data.inGameData.storyList;
        DataManager.Single.Save();
    }

    List<int> selectList;

    GameObject panel;

    public bool isSelect;

    public bool FirstCheck(TextInfo textInfo) // 텍스트가 나오기 전에 적용
    {
        if (target == "UseCard")
        {
            useCardPanel.SetActive(true);
            CardManager.instance.isUseCard = true;
            return true;
        }
        if (target == "BatEvent")
        {
            useCardPanel.SetActive(true);
            CardManager.instance.isUseCard = true;
            CardManager.instance.isBatCard = true;
            return true;
        }
        if (textInfo.IsBranchChange)
        {
            isSelect = true;

            selectList = new List<int>();
            string[] texts = textInfo.Text.Split('\n');
            panel.SetActive(true);

            if(textInfo.BranchCount == 1)
            {
                select[0].transform.localPosition = new Vector3(0, 500, 0);
                select[0].SetActive(true);
                select[0].transform.GetChild(1).GetComponent<TMP_Text>().text = texts[0];
                if (!CheckItem(textInfo.Branch1) && (textInfo.Branch1 != "null"))
                    select[0].transform.GetChild(0).gameObject.SetActive(true);
                else
                    selectList.Add(1);
            }
            if (textInfo.BranchCount == 2)
            {
                select[0].transform.localPosition = new Vector3(0, 705, 0);
                select[1].transform.localPosition = new Vector3(0, 395, 0);
                select[0].SetActive(true);
                select[1].SetActive(true);
                select[0].transform.GetChild(1).GetComponent<TMP_Text>().text = texts[0];
                select[1].transform.GetChild(1).GetComponent<TMP_Text>().text = texts[1];

                if (!CheckItem(textInfo.Branch1) && (textInfo.Branch1 != "null"))
                    select[0].transform.GetChild(0).gameObject.SetActive(true);
                else
                    selectList.Add(1);
                if (!CheckItem(textInfo.Branch2) && (textInfo.Branch2 != "null"))
                    select[1].transform.GetChild(0).gameObject.SetActive(true);
                else
                    selectList.Add(2);

            }
            if (textInfo.BranchCount == 3)
            {
                select[0].transform.localPosition = new Vector3(0, 755, 0);
                select[1].transform.localPosition = new Vector3(0, 500, 0);
                select[2].transform.localPosition = new Vector3(0, 245, 0);
                select[0].SetActive(true);
                select[1].SetActive(true);
                select[2].SetActive(true);
                select[0].transform.GetChild(1).GetComponent<TMP_Text>().text = texts[0];
                select[1].transform.GetChild(1).GetComponent<TMP_Text>().text = texts[1];
                select[2].transform.GetChild(1).GetComponent<TMP_Text>().text = texts[2];

                if (!CheckItem(textInfo.Branch1) && (textInfo.Branch1 != "null"))
                    select[0].transform.GetChild(0).gameObject.SetActive(true);
                else
                    selectList.Add(1);
                if (!CheckItem(textInfo.Branch2) && (textInfo.Branch2 != "null"))
                    select[1].transform.GetChild(0).gameObject.SetActive(true);
                else
                    selectList.Add(2);
                if (!CheckItem(textInfo.Branch3) && (textInfo.Branch3 != "null"))
                    select[2].transform.GetChild(0).gameObject.SetActive(true);
                else
                    selectList.Add(3);
            }

            return true;
        }

        return false;
    }

    public void SelectSelect(int num)
    {
        if(!selectList.Contains(num))
        {
            return;
        }
        panel.SetActive(false);
        select[0].transform.GetChild(0).gameObject.SetActive(false);
        select[1].transform.GetChild(0).gameObject.SetActive(false);
        select[2].transform.GetChild(0).gameObject.SetActive(false);
        select[0].SetActive(false);
        select[1].SetActive(false);
        select[2].SetActive(false);

        branch = num;
        StartStory(DataManager.Single.Data.inGameData.storyList[DataManager.Single.Data.inGameData.storyCnt], branch);
        isSelect = false;
    }

    bool CheckItem(string name)
    {
        foreach(Item item in DataManager.Single.Data.inGameData.myItemList)
        {
            if(item.name == name)
            {
                return true;
            }
        }

        return false;
    }

    [SerializeField]
    public GameObject useCardPanel;

    public void LastCheck(TextInfo textInfo) // 텍스트가 나온 후에 적용
    {
        if (IsItem(target))
        {
            Item tempItem = new Item();
            foreach (Item item in itemSO.items)
            {
                if (target == item.name) tempItem = item;
            }
            CardManager.instance.SpawnRope(new List<Item> { tempItem }, 0.2f);
        }
        #region HPMP
        if (target == "Hp1-")
        {
            DataManager.Single.Data.inGameData.hp -= 1;
            ShakeScreen();
            HpMpSetting();
        }
        if (target == "Hp2-")
        {
            DataManager.Single.Data.inGameData.hp -= 2;
            ShakeScreen();
            HpMpSetting();
        }
        if (target == "Hp3-")
        {
            DataManager.Single.Data.inGameData.hp -= 3;
            ShakeScreen();
            HpMpSetting();
        }
        if (target == "Mp1-")
        {
            DataManager.Single.Data.inGameData.mp -= 1;
            ShakeScreen();
            HpMpSetting();
        }
        if (target == "Mp2-")
        {
            DataManager.Single.Data.inGameData.mp -= 2;
            ShakeScreen();
            HpMpSetting();
        }
        if (target == "Mp3-")
        {
            DataManager.Single.Data.inGameData.mp -= 3;
            ShakeScreen();
            HpMpSetting();
        }
        if (target == "Hp1+")
        {
            DataManager.Single.Data.inGameData.hp += 1;
            HpMpSetting();
        }
        if (target == "Hp2+")
        {
            DataManager.Single.Data.inGameData.hp += 2;
            HpMpSetting();
        }
        if (target == "Hp3+")
        {
            DataManager.Single.Data.inGameData.hp += 3;
            HpMpSetting();
        }
        if (target == "Mp1+")
        {
            DataManager.Single.Data.inGameData.mp += 1;
            HpMpSetting();
        }
        if (target == "Mp2+")
        {
            DataManager.Single.Data.inGameData.mp += 2;
            HpMpSetting();
        }
        if (target == "Mp3+")
        {
            DataManager.Single.Data.inGameData.mp += 3;
            HpMpSetting();
        }
        #endregion
        if (textInfo.End)
        {
            if (target == "Ending")
            {
                isStoryEnd = true;
                DataManager.Single.Data.saveData.endingList[textInfo.EndIndex] = textInfo.EndName;
                DataManager.Single.Save();
                quit.SetActive(true);
                reGame.SetActive(true);
            }
            else
            {
                isEnd = true;
                isStoryEnd = true;
                EndStory();
            }
        }
    }


    bool IsItem(string name)
    {
        foreach(Item item in itemSO.items)
        {
            if(name == item.name) return true;
        }
        return false;
    }

    void GetItem(string name)
    {
        CardManager.instance.FindAndAddCard(name);
    }

    public void ShakeScreen()
    {
        GameObject.Find("Main(Clone)").transform.DOShakePosition(0.5f, new Vector3(50, 50, 0));
    }

    public List<TextInfo> GetTextInfo(int num, int branch)
    {
        List<TextInfo> list = new List<TextInfo>();

        foreach(TextInfo textInfo in DataManager.Single.Data.textData.textInfo)
        {
            if(textInfo.Case == num)
            {
                if(textInfo.Branch == branch)
                {
                    list.Add(textInfo);
                }
            }
        }

        return list;
    }

    int lastHp;
    int lastMp;
    public bool isGameOver;

    public void HpMpSetting()
    {
        GameObject hp = GameObject.Find("HP");

        if(DataManager.Single.Data.inGameData.hp >= 3)
        {
            DataManager.Single.Data.inGameData.hp = 3;
        }
        if (DataManager.Single.Data.inGameData.mp >= 3)
        {
            DataManager.Single.Data.inGameData.mp = 3;
        }
        if (DataManager.Single.Data.inGameData.hp <= 0)
        {
            isGameOver = true;
        }
        if (DataManager.Single.Data.inGameData.mp <= 0)
        {
            DataManager.Single.Data.inGameData.mp = 0;
        }

        if (lastHp > DataManager.Single.Data.inGameData.hp)
        {
            if(lastHp != 0)
            {
                for (int i = lastHp - 1; i >= DataManager.Single.Data.inGameData.hp; i--)
                {
                    StartCoroutine(HPMPDecrease(hp.transform.GetChild(i).GetComponent<UnityEngine.UI.Image>()));
                }
            }
        }
        else
        {
            for (int i = lastHp; i < DataManager.Single.Data.inGameData.hp; i++)
            {
                StartCoroutine(HPMPUp(hp.transform.GetChild(i).GetComponent<UnityEngine.UI.Image>()));
            }
        }

        GameObject mp = GameObject.Find("MP");

        if (lastMp > DataManager.Single.Data.inGameData.mp)
        {
            if(lastMp != 0)
            {
                for (int i = lastMp - 1; i >= DataManager.Single.Data.inGameData.mp; i--)
                {
                    StartCoroutine(HPMPDecrease(mp.transform.GetChild(i).GetComponent<UnityEngine.UI.Image>()));
                }
            }
        }
        else
        {
            for (int i = lastMp; i < DataManager.Single.Data.inGameData.mp; i++)
            {
                StartCoroutine(HPMPUp(mp.transform.GetChild(i).GetComponent<UnityEngine.UI.Image>()));
            }
        }

        lastHp = DataManager.Single.Data.inGameData.hp;
        lastMp = DataManager.Single.Data.inGameData.mp;
    }

    IEnumerator HPMPDecrease(UnityEngine.UI.Image image)
    {
        image.fillAmount = 1;
        while (image && image.fillAmount > 0)
        {
            image.fillAmount -= Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator HPMPUp(UnityEngine.UI.Image image)
    {
        image.fillAmount = 0;
        while (image && image.fillAmount < 1)
        {
            image.fillAmount += Time.deltaTime;
            yield return null;
        }
    }

    public void NextText()
    {
        MainController.main.sound.Play("Click");
        if (!isTextEnd)
        {
            if(textShow != null)
            {
                StopCoroutine(textShow);
                texts[texts.Count - 1].GetComponent<TMP_Text>().text = textInfo[cnt].Text;
                LastCheck(textInfo[cnt]);
                isTextEnd = true;
            }
            return;
        }
        if(isStoryEnd)
        {
            return;
        }
        if(isSelect)
        {
            return;
        }
        if(isEnd)
        {
            return;
        }
        isTextEnd = false;
        cnt++;
        textShow = StartCoroutine(TextShow(textInfo[cnt]));
    }

    string lastImage;

    public IEnumerator TextShow(TextInfo textInfo)
    {
        if(isGameOver)
        {
            textInfo = new TextInfo();
            textInfo = DataManager.Single.Data.textData.textInfo[DataManager.Single.Data.textData.textInfo.Count - 1];
        }
        if(lastImage == textInfo.Image)
        {
        }
        else
        {
            BGImage.GetComponent<UnityEngine.UI.Image>().DOFade(0f, 1).OnComplete(()=> {
                if (textInfo.Image != "null")
                {
                    Debug.Log(MainController.main.resource.sprite[textInfo.Image].name);
                    BGImage.GetComponent<UnityEngine.UI.Image>().sprite = MainController.main.resource.sprite[textInfo.Image];
                    BGImage.GetComponent<UnityEngine.UI.Image>().DOFade(1.0f, 1);
                }
            });
        }

        lastImage = textInfo.Image;

        if(blink != null)
        {
            Destroy(blink);
        }

        target = textInfo.Target;

        if (FirstCheck(textInfo))
        {
            
        }
        else
        {
            target = textInfo.Target;

            GameObject tempObj = Instantiate(text, Vector3.zero, Util.QI);

            TMP_Text tempText = tempObj.GetComponent<TMP_Text>();
            tempText.text = "";
            tempObj.transform.SetParent(textBox.transform, false);
            texts.Add(tempObj);

            blink = Instantiate(blinkText, Vector3.zero, Util.QI);
            blink.transform.SetParent(textBox.transform, false);

            if(texts.Count != 1)
                textBox.transform.parent.parent.GetComponent<ScrollRect>().normalizedPosition = new Vector2(textBox.transform.parent.parent.GetComponent<ScrollRect>().normalizedPosition.x, 0);
            else
                textBox.transform.parent.parent.GetComponent<ScrollRect>().normalizedPosition = new Vector2(textBox.transform.parent.parent.GetComponent<ScrollRect>().normalizedPosition.x, 1);
            Debug.Log(textBox.transform.parent.parent.GetComponent<ScrollRect>().normalizedPosition);
            tempObj.SetActive(false);
            tempText.text = textInfo.Text;
            tempObj.GetComponent<RectTransform>().sizeDelta = new Vector2(900, tempText.preferredHeight);
            tempText.text = "";
            tempObj.SetActive(true);

            StringBuilder sb = new StringBuilder();
            WaitForSeconds delay = new WaitForSeconds(0.03f);

            for (int i = 0; i < textInfo.Text.Length; i++)
            {
                yield return delay;
                sb.Append(textInfo.Text[i]);

                tempText.text = sb.ToString();
            }
            LastCheck(textInfo);
            isTextEnd = true;
        }
    }

    List<int> StoryMake() // 0 메인, 1 카드 획득, 2 카드 소비, 3 스탯 획득, 4 스탯 감소
    {
        List<int> storyList = new List<int>();

        List<int> copyTable0 = IntDeepCopy(table0);
        List<int> copyTable1 = IntDeepCopy(table1);
        List<int> copyTable2 = IntDeepCopy(table2);
        List<int> copyTable3 = IntDeepCopy(table3);
        List<int> copyTable4 = IntDeepCopy(table4);

        storyList.Add(copyTable0[0]);
        storyList.Add(copyTable0[1]);
        storyList.Add(Random1(copyTable1));
        storyList.Add(Random1(copyTable1));
        storyList.Add(Random1(copyTable4));
        storyList.Add(Random1(copyTable3));
        storyList.Add(Random1(copyTable1));
        storyList.Add(Random1(copyTable4));
        storyList.Add(copyTable0[2]);
        storyList.Add(Random1(copyTable1));
        storyList.Add(Random1(copyTable4));
        storyList.Add(Random1(copyTable3));
        storyList.Add(Random1(copyTable2));
        storyList.Add(copyTable0[3]);
        storyList.Add(Random2(copyTable1, copyTable2, copyTable3, copyTable4));
        storyList.Add(Random2(copyTable1, copyTable2, copyTable3, copyTable4));
        storyList.Add(Random2(copyTable1, copyTable2, copyTable3, copyTable4));
        storyList.Add(Random2(copyTable1, copyTable2, copyTable3, copyTable4));
        storyList.Add(Random2(copyTable1, copyTable2, copyTable3, copyTable4));
        storyList.Add(Random2(copyTable1, copyTable2, copyTable3, copyTable4));
        storyList.Add(Random2(copyTable1, copyTable2, copyTable3, copyTable4));
        storyList.Add(Random2(copyTable1, copyTable2, copyTable3, copyTable4));
        storyList.Add(copyTable0[4]);

        return storyList;
    }

    int Random1(List<int> list)
    {
        int num = UnityEngine.Random.Range(0, list.Count);
        int result = list[num];
        list.RemoveAt(num);

        return result;
    }

    int Random2(List<int> table1, List<int> table2, List<int> table3, List<int> table4)
    {
        List<int> list = new List<int>();
        int random = UnityEngine.Random.Range(1, 5);
        while(list.Count == 0)
        {
            if (random == 1)
            {
                list = table1;
            }
            if (random == 2)
            {
                list = table2;
            }
            if (random == 3)
            {
                list = table3;
            }
            if (random == 4)
            {
                list = table4;
            }
        }

        int num = UnityEngine.Random.Range(0, list.Count);
        int result = list[num];
        list.RemoveAt(num);

        return result;
    }

    List<int> IntDeepCopy(List<int> ori)
    {
        List<int > cpy = new List<int>();

        foreach(int i in ori)
        {
            cpy.Add(i);
        }

        return cpy;
    }
}
