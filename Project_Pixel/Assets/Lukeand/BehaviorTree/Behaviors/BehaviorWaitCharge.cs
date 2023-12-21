using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class BehaviorWaitCharge : Node
{
    EnemyBase enemy;
    float total;
    float current;

    public BehaviorWaitCharge(EnemyBase enemy, float total)
    {
        this.enemy = enemy;
        this.total = total; ;
        current = 0;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("wait charge");
        enemy.MoveHorizontal(0);
        if(total > current)
        {
            //ready animation.          
            enemy.ReadyAnimation();
            current += Time.deltaTime;          
            return NodeState.Running;
        }
        else
        {
            Debug.Log("done");
            current = 0;
            return NodeState.Success;
            
        }
    }
}
