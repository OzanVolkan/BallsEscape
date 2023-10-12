using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBox : SingletonManager<RewardBox>
{
    [SerializeField] private Transform[] ballSlots;
    public Transform[] BallSlots
    {
        get { return ballSlots; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
