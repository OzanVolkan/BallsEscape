using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RewardBox : SingletonManager<RewardBox>
{
    [SerializeField] private Transform[] ballSlots;
    public Transform[] BallSlots
    {
        get { return ballSlots; }
    }

    public void ShowRewardBox()
    {
        Vector3 newPos = new Vector3(0f, 2f, 0f);
        transform.DOMove(newPos, 1f).SetEase(Ease.OutBack);
    }
}
