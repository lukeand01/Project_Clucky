using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPig : EnemyBase
{
    [Separator("PIG")]
    [SerializeField] float waitToCharge;
    [SerializeField] float chargeDistance;
    [SerializeField] float chargeSpeedModifier = 2.5f;

    protected override void SetUpBehavior()
    {
        base.SetUpBehavior();
        UpdateTree(GetPigBehavior());
    }
  

    Sequence2 GetPigBehavior()
    {
        return new Sequence2(new List<Node>
        {
            new BehaviorPatrol(this, patrolDistance, 0),
            new BehaviorDetect(this),
            new BehaviorReady(this, waitToCharge),
            new BehaviorCharge(this, chargeDistance, chargeSpeedModifier),
            new BehaviorWait(this)
        });
    }

}
