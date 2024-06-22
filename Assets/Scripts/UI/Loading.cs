using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField]
    Image loadingBar;
    [SerializeField]
    Image card;
    [SerializeField]
    TMP_Text text;
    [SerializeField]
    ItemSO itemSO;

    void Start()
    {
        Time.timeScale = 1;
        DOTween.timeScale = 1;
        if(DataManager.Single.Data.inGameData.isMain)
        {
            InGameManager.instance.GameStart();
        }
        int num = UnityEngine.Random.Range(0, 9);
        card.sprite = itemSO.items[num].sprite;
        text.text = itemSO.items[num].description;

        StartCoroutine(LoadingStart());
    }

    IEnumerator LoadingStart()
    {
        while(loadingBar.fillAmount < 1)
        {
            loadingBar.fillAmount += Time.deltaTime;
            yield return null;
        }

        if (DataManager.Single.Data.inGameData.isMain)
        {
            MainController.main.UI.UIsetting(Define.UIlevel.Level3, Define.UItype.MainUI);
        }
        else
        {
            MainController.main.UI.DestroyUI(Define.UIlevel.Level3);
        }
    }
}
