using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorReady : Node
{
    //this is before attacking.
    //

    EnemyBase enemy;
    float total;
    float current;

    public BehaviorReady(EnemyBase enemy, float total)
    {
        this.enemy = enemy;
        this.total = total; ;
        current = 0;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("wait charge");
        enemy.MoveHorizontal(0);
        if (total > current)
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
