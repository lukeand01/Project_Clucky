using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorBeeAttack : Node
{
    EnemyBase enemy;
    public BehaviorBeeAttack(EnemyBase enemy)
    {
        this.enemy = enemy;
    }

    //shoot at positions.
    //stand still.

    public override NodeState Evaluate()
    {
        return base.Evaluate();
    }

}
