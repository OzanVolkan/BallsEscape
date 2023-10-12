using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : SingletonManager<AnimationManager>
{
    private Animator gridalAnimator, rewardBoxAnimator, ballBoxAnimator;

    private void Start()
    {
        gridalAnimator = GameObject.Find("Gridal").GetComponent<Animator>();
        rewardBoxAnimator = GameObject.Find("RewardBox").GetComponent<Animator>();
        ballBoxAnimator = GameObject.Find("BallBox").GetComponent<Animator>();
    }
    public void GridalShake(string _dir)
    {
        gridalAnimator.SetTrigger(_dir);
    }

    public void RewardBoxTurning()
    {
        rewardBoxAnimator.SetTrigger("Turn");
    }

    public void BallBoxWin()
    {
        ballBoxAnimator.SetTrigger("Win");
    }
}
