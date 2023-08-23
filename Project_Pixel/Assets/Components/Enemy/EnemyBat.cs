using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : EnemyBase
{

    protected override void SetUpBehavior()
    {
        base.SetUpBehavior();
        UpdateTree(GetBehavior());
    }

    Sequence2 GetBehavior()
    {
        return new Sequence2(new List<Node>
        {            
            new BehaviorDetect(this),            
            new BehaviorReady(this, 0.5f),
            new BehaviorBatAttack(this)
        });
    }

}