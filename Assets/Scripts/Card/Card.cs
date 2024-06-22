using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer card;

    public Item item;
    public PRS originPRS;
    
    public void Setup(Item item)
    {
        this.item = item;

        card.sprite = item.sprite;
    }

    public void MoveTransform(PRS prs, bool useDoteen, float dotweenTime = 0)
    {
        if(useDoteen)
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }

    void OnMouseDown()
    {
        MainController.main.sound.Play("Click");
        CardManager.instance.CardMouseDown(this);
    }

    void OnMouseUp()
    {
        CardManager.instance.CardMouseUp(this);
    }
}
