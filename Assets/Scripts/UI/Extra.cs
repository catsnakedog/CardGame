using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Extra : MonoBehaviour
{
    [SerializeField]
    List<GameObject> textBox;

    private void Start()
    {
        for(int i = 0; i < 7; i++)
        {
            if (DataManager.Single.Data.saveData.endingList[i] == "")
            {
                textBox[i].transform.GetChild(0).GetComponent<TMP_Text>().text = "???????";
            }
            else
            {
                textBox[i].transform.GetChild(0).GetComponent<TMP_Text>().text = DataManager.Single.Data.saveData.endingList[i];
            }
        }
    }
}
