using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorAttack : Node
{
    EnemyBase enemy;
    

    bool isInit;
    float currentCooldown;
    float totalCooldown;

    public BehaviorAttack(EnemyBase enemy)
    {
        this.enemy = enemy;
    }

    public override NodeState Evaluate()
    {
        //while its attacking we waiting.
        //rotate to thee right side?

        if (!isInit)
        {

            isInit = true;
            enemy.CompleteStop();
            enemy.AttackAnimation();
        }      

        if (enemy.IsAttacking())
        {

            return NodeState.Running;
        }


        isInit = false;
        
        return NodeState.Success;
    }
}
