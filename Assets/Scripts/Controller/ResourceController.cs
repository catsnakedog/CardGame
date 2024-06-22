using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ResourceController : MonoBehaviour
{
    public List<GameObject> UItype { get; set; }
    public Dictionary<string, Sprite> sprite { get; set; }

    public void init()
    {
        UItypeSetting();
        SpriteSetting();
    }

    void UItypeSetting()
    {
        UItype = new List<GameObject>();
        for (int i = 0; i < (int)Define.UItype.MaxCount; i++)
        {
            UItype.Add(Resources.Load<GameObject>("Prefabs/UI/" + Enum.GetName(typeof(Define.UItype), i)));
        }
    }

    void SpriteSetting()
    {
        sprite = new Dictionary<string, Sprite>();

        for (int i = 0; i < (int)Define.SpriteDict.MaxCount; i++)
        {
            sprite.Add(Enum.GetName(typeof(Define.SpriteDict), i), Resources.Load<Sprite>("Sprite/" + Enum.GetName(typeof(Define.SpriteDict), i)));
        }
    }
}