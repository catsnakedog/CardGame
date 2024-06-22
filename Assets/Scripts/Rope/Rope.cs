using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] SelectCard selectCard;
    [SerializeField] Ease moveEffect;
    

    public void MoveTransform(float num)
    {
        if (num < 0)
        {
            moveEffect = Ease.InQuint;
        }
        else
        {
            selectCard.gameObject.transform.Rotate(new Vector3(0, 0, 1f) * UnityEngine.Random.Range(-15f, 15f));
            moveEffect = Ease.OutQuint;
        }
        transform.DOMove(new Vector3(transform.position.x, transform.position.y - num, transform.position.z), 0.5f).SetEase(moveEffect);
    }

    public void Setup(Item item)
    {
        selectCard.Setup(item);
    }
}
