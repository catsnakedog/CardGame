using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    void Awake() => instance = this;

    [SerializeField] ItemSO itemSO;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] GameObject RopePrefab;
    [SerializeField] public GameObject RopePanel;
    [SerializeField] List<Card> myCard;
    [SerializeField] List<GameObject> myRope;
    [SerializeField] Transform cardSpawnPoint;
    [SerializeField] Transform cardLeft;
    [SerializeField] Transform cardRight;

    [SerializeField] Card selectCard;

    [SerializeField] public bool isFirstCard;

    private void Start()
    {
        isFirstCard = false;
        myCard = new List<Card>();
        prs = new PRS(new Vector3(), Util.QI, Vector3.zero);
        DOTween.SetTweensCapacity(2000, 100);
    }

    public void GameReset()
    {
        DOTween.KillAll();
        DOTween.Clear();
        StopAllCoroutines();

        foreach (Card card in myCard)
        {
            Destroy(card.gameObject);
        }

        myCard.Clear();

        foreach(GameObject obj in myRope)
        {
            Destroy(obj);
        }

        myRope.Clear();

        selectCard = null;
        isFirstCard = true;
        costLow.SetActive(false);
        isDrag = false;
        isUseCard = false;
        isBatCard = false;
    }

    public void SpawnSelectCard(string name)
    {
        Item selectCard;
        foreach(Item item in itemSO.items)
        {
            if(item.name == name)
            {
                selectCard = item;
                break;
            }
        }
    }
    
    public void SpawnRope(List<Item> items, float delay)
    {
        RopePanel.SetActive(true);
        myRope.Clear();
        MainController.main.sound.Play("Rope");

        List<Vector3> spawnPoint = new List<Vector3>();

        if(items.Count == 1)
        {
            spawnPoint.Add(new Vector3(0f, 8f, -100f));
            myRope = new List<GameObject>();
        }
        else if(items.Count == 2)
        {
            spawnPoint.Add(new Vector3(-1f, 8f, -100f));
            spawnPoint.Add(new Vector3(1f, 8f, -100f));
        }
        else if(items.Count == 3)
        {
            spawnPoint.Add(new Vector3(-1.5f, 8f, -100f));
            spawnPoint.Add(new Vector3(0f, 8f, -100f));
            spawnPoint.Add(new Vector3(1.5f, 8f, -100f));
        }

        for(int i = 0; i < items.Count; i++)
        {
            var lopeObject = Instantiate(RopePrefab, spawnPoint[i], Util.QI);
            var lope = lopeObject.GetComponent<Rope>();
            lope.Setup(items[i]);
            myRope.Add(lopeObject);
        }

        StartCoroutine(ShowRope(delay));
    }

    public IEnumerator ShowRope(float delay)
    {
        List<GameObject> ropes = myRope;

        foreach(GameObject rope in ropes)
        {
            rope.GetComponent<Rope>().MoveTransform(4f);
            yield return new WaitForSeconds(delay);
        }
    }

    public void DeleteRope(float delay)
    {
        StartCoroutine(Delete(delay));
        if(isFirstCard)
        {
            isFirstCard = false;
            InGameManager.instance.StartStory(DataManager.Single.Data.inGameData.storyList[DataManager.Single.Data.inGameData.storyCnt], 0);
        }
    }

    public IEnumerator Delete(float delay)
    {
        List<GameObject> ropes = myRope;

        foreach (GameObject rope in ropes)
        {
            rope.GetComponent<Rope>().MoveTransform(-4f);
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(2f);

        foreach (GameObject rope in ropes)
        {
            rope.transform.DOKill();
            Destroy(rope);
        }
    }

    public void FindAndAddCard(string name)
    {
        var cardObject = Instantiate(cardPrefab, cardSpawnPoint.position, Util.QI);
        var card = cardObject.GetComponent<Card>();

        Item targetItem = new Item();

        foreach(Item a in itemSO.items)
        {
            if(a.name == name)
            {
                targetItem = a;
                break;
            }
        }

        DataManager.Single.Data.inGameData.myItemList.Add(targetItem);
        card.Setup(targetItem);
        myCard.Add(card);
        if(name != "Кѓ Ме")
            MainController.main.sound.Play("CardGet");

        SetOriginOrder();
        CardAlignment();
    }

    [SerializeField]
    public GameObject costLow;

    public void DeleteCard()
    {
        if(selectCard.GetComponent<Card>().item.name == "Кѓ Ме")
        {
            SetOriginOrder();
            CardAlignment();
            return;
        }

        DataManager.Single.Data.inGameData.myItemList.Remove(selectCard.item);
        myCard.Remove(selectCard);
        Destroy(selectCard.gameObject);

        SetOriginOrder();
        CardAlignment();
    }


    void SetOriginOrder()
    {
        int count = myCard.Count;
        for (int i = 0; i < count; i++)
        {
            var targetCard = myCard[i];
            targetCard.GetComponent<Order>().SetOriginOrder(i);
        }
    }

    void CardAlignment()
    {
        DOTween.KillAll();
        DOTween.Clear();

        List<PRS> originCardPRS;

        originCardPRS = RoundAlignment(cardLeft, cardRight, myCard.Count, 0.5f, Vector3.one * 0.13f);

        for (int i = 0; i < myCard.Count; i++)
        {
            var targetCard = myCard[i];

            targetCard.originPRS = originCardPRS[i];
            targetCard.MoveTransform(targetCard.originPRS, true, 0.7f);
        }
    }

    void TargetCardAlignment(Card card)
    {
        DOTween.KillAll();
        DOTween.Clear();

        List<PRS> originCardPRS;

        originCardPRS = TargetRoundAlignment(cardLeft, cardRight, myCard.Count, 0.5f, Vector3.one * 0.13f, card);

        for (int i = 0; i < myCard.Count; i++)
        {
            var targetCard = myCard[i];

            targetCard.originPRS = originCardPRS[i];
            targetCard.MoveTransform(targetCard.originPRS, true, 0.7f);
        }
    }

    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale)
    {
        float[] objLerps = new float[objCount];
        List<PRS> results = new List<PRS>(objCount);

        switch (objCount)
        {
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0.27f, 0.73f }; break;
            case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
            default:
                float interval = 1f / (objCount - 1);
                for (int i = 0; i < objCount; i++)
                    objLerps[i] = interval * i;
                break;
        }

        for (int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Util.QI;
            if (objCount >= 4)
            {
                float curve = Mathf.Sqrt(Mathf.Abs(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2)));
                curve = height >= 0 ? curve : -curve;
                targetPos.y += curve;
                targetPos.y -= 0.33f;
                targetPos.z -= 10 * i;

                if(targetPos.y < -4.5f)
                {
                    targetPos.y = -4.5f;
                }

                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
            }

            results.Add(new PRS(targetPos, targetRot, scale));
        }

        return results;
    }

    List<PRS> TargetRoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale, Card target)
    {
        float[] objLerps = new float[objCount];
        List<PRS> results = new List<PRS>(objCount);

        int targetIdx = myCard.IndexOf(target);
        float targetInterval = 0.35f;

        switch (objCount)
        {
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0.27f, 0.73f }; break;
            case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
            default:
                if (targetIdx == 0)
                {
                    float interval = (1f-targetInterval) / (objCount - 2);
                    objLerps[0] = 0f;
                    objLerps[1] = targetInterval;
                    for (int i = 2; i < objCount; i++)
                        objLerps[i] = objLerps[i - 1] + interval;
                    break;
                }
                if (targetIdx == objCount - 1)
                {
                    float interval = (1f - targetInterval) / (objCount - 2);
                    for (int i = 0; i < targetIdx; i++)
                        objLerps[i] = i * interval;
                    objLerps[targetIdx] = objLerps[targetIdx - 1] + targetInterval;
                    break;
                }
                else
                {
                    float interval = (1f - targetInterval) / (objCount - 2);
                    for (int i = 0; i < targetIdx + 1; i++)
                        objLerps[i] = i * interval;
                    objLerps[targetIdx + 1] = objLerps[targetIdx] + targetInterval;
                    for (int i = targetIdx + 2; i < objCount; i++)
                        objLerps[i] = objLerps[i - 1] + interval;
                    break;
                }
        }

        for (int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Util.QI;
            if (objCount >= 4)
            {
                float curve = Mathf.Sqrt(Mathf.Abs(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2)));
                curve = height >= 0 ? curve : -curve;
                targetPos.y += curve;
                targetPos.y -= 0.33f;
                targetPos.z -= 10 * i;

                if (targetPos.y < -4.5f)
                {
                    targetPos.y = -4.5f;
                }

                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
            }

            results.Add(new PRS(targetPos, targetRot, scale));
        }

        return results;
    }

    bool isDrag;
    Vector3 mousePos;
    PRS prs;

    private void Update()
    {
        if (isDrag)
        {
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            mousePos.z = -200;
            mousePos.y += 1f;

            prs.pos = mousePos;
            prs.rot = Util.QI;
            prs.scale = Vector3.one * 0.19f;

            selectCard.MoveTransform(prs, true, 0.15f);
        }
    }

    [SerializeField] public bool isUseCard;
    [SerializeField] public bool isBatCard;

    public void CardMouseDown(Card card)
    {
        if (card == selectCard)
        {
            if (isUseCard)
            {
                isDrag = true;
            }
        }
        else
            isDrag = false;

        selectCard = card;
        TargetCardAlignment(card);
    }

    public void CardMouseUp(Card card)
    {
        if(isDrag)
        {
            if ((mousePos.y -= 1f) >= 0f)
            {
                if (selectCard.item.cost > DataManager.Single.Data.inGameData.mp)
                {
                    costLow.GetComponent<Image>().DOFade(1f, 1f).OnComplete(() => { costLow.GetComponent<Image>().DOFade(0f, 1f); });
                    return;
                }
                else
                {
                    DataManager.Single.Data.inGameData.mp -= selectCard.item.cost;
                    InGameManager.instance.HpMpSetting();
                }

                isDrag = false;

                if (isBatCard)
                {
                    if (selectCard.item.name == "Кѓ Ме")
                    {
                        InGameManager.instance.branch = 2;
                    }
                    else
                    {
                        DeleteCard();
                        InGameManager.instance.branch = 1;
                    }
                    isUseCard = false;
                    isBatCard = false;
                    InGameManager.instance.useCardPanel.SetActive(false);
                    InGameManager.instance.StartStory(DataManager.Single.Data.inGameData.storyList[DataManager.Single.Data.inGameData.storyCnt], InGameManager.instance.branch);
                    return;
                }

                if (selectCard.item.name == InGameManager.instance.textInfo[InGameManager.instance.cnt].Branch1 ||
                    InGameManager.instance.textInfo[InGameManager.instance.cnt].Branch1 == "null")
                {
                    DeleteCard();
                    InGameManager.instance.branch = 1;
                }
                else if (selectCard.item.name == InGameManager.instance.textInfo[InGameManager.instance.cnt].Branch2 ||
                    InGameManager.instance.textInfo[InGameManager.instance.cnt].Branch2 == "null")
                {
                    DeleteCard();
                    InGameManager.instance.branch = 2;
                }
                else if (selectCard.item.name == InGameManager.instance.textInfo[InGameManager.instance.cnt].Branch3 ||
                    InGameManager.instance.textInfo[InGameManager.instance.cnt].Branch3 == "null")
                {
                    DeleteCard();
                    InGameManager.instance.branch = 3;
                }

                isUseCard = false;
                InGameManager.instance.useCardPanel.SetActive(false);
                InGameManager.instance.StartStory(DataManager.Single.Data.inGameData.storyList[DataManager.Single.Data.inGameData.storyCnt], InGameManager.instance.branch);
            }
            isDrag = false;
            CardAlignment();
        }
    }
}
