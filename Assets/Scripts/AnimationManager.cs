using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : SingletonManager<AnimationManager>
{
    private Animator gridalAnimator;

    private void Start()
    {
        gridalAnimator = GameObject.Find("Gridal").GetComponent<Animator>();
    }
    public void GridalShake(string _dir)
    {
        gridalAnimator.SetTrigger(_dir);
    }
}
