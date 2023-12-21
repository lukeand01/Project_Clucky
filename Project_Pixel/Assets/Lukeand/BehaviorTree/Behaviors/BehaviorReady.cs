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

    bool isInit;

    public BehaviorReady(EnemyBase enemy, float total)
    {
        this.enemy = enemy;
        this.total = total; ;
        current = 0;
    }

    public override NodeState Evaluate()
    {
        //turn it off.
        enemy.ControlAudioSource(false);
        enemy.MoveHorizontal(0);

        if (!isInit)
        {
            enemy.DoAudioClip(0, 15);
            isInit = true;
        }

        if (total > current)
        {
            //ready animation.          
            enemy.ReadyAnimation();
            current += Time.deltaTime;
            return NodeState.Running;
        }
        else
        {
            current = 0;
            return NodeState.Success;

        }
    }

}
