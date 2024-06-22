using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelController : MonoBehaviour
{
    MainController main;

    public List<GameObject> Levels = new List<GameObject>();

    public void init()
    {
        main = MainController.main;
        Levels.Add(GameObject.FindWithTag("Level1"));
        Levels.Add(GameObject.FindWithTag("Level2"));
        Levels.Add(GameObject.FindWithTag("Level3"));
    }

    public void UIsetting(Define.UIlevel UIlevel, Define.UItype UItype)
    {
        Transform[] childList = Levels[(int)UIlevel].GetComponentsInChildren<Transform>();
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != Levels[(int)UIlevel].transform) Destroy(childList[i].gameObject);
            }
        }
        GameObject target = main.resource.UItype[(int)UItype];
        Instantiate(target, target.transform.position, Quaternion.identity).transform.SetParent(Levels[(int)UIlevel].transform, false);
    }

    public void DestroyUI(Define.UIlevel UIlevel)
    {
        Transform[] childList = Levels[(int)UIlevel].GetComponentsInChildren<Transform>();
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != Levels[(int)UIlevel].transform) Destroy(childList[i].gameObject);
            }
        }
    }
}