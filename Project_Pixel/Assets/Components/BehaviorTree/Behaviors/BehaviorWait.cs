using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorWait : Node
{
    EnemyBase enemy;
    float current;
    public BehaviorWait(EnemyBase enemy)
    {
        this.enemy = enemy;
    }


    public override NodeState Evaluate()
    {
        if(current >= enemy.totalCooldown)
        {
            current += Time.deltaTime;
            return NodeState.Running;
        }
        else
        {
            return NodeState.Success;
        }
    }
}
