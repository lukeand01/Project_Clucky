using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyChameleon : EnemyBase
{
    [SerializeField] Sword sword;

    

    protected override void SetUpBehavior()
    {

        base.SetUpBehavior();
        UpdateTree(GetChamelleonBehavior());

    }

    protected override void UpdateFunction()
    {
        base.UpdateFunction();
    }

    Sequence2 GetChamelleonBehavior()
    {

        return new Sequence2(new List<Node>
        {
            new BehaviorPatrol(this, patrolDistance, 0),
            new BehaviorDetect(this),
            new BehaviorLineOfSight(this, 2.1f),
            new BehaviorAttack(this, 0)
        });
    }


}
