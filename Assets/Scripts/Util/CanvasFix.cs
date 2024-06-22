using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float setWidth = 1080; // ����� ���� �ʺ�
        float setHeight = 2400; // ����� ���� ����

        float deviceWidth = Screen.width; // ��� �ʺ� ����
        float deviceHeight = Screen.height; // ��� ���� ����

        float rate = (setHeight / setWidth);

        if (rate * deviceWidth > deviceHeight)// ���ΰ� �� �� ��Ȳ (���ο� �������)
        {
            this.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
        }
        else // ���ΰ� �� �� ��Ȳ (���ο� �������)
        {
            this.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
        }
    }
}
