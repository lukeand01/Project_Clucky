using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorBatAttack : Node
{
    //
    EnemyBase enemy;
    bool isInit;

    Vector3 targetDir;
    Vector3 originalPos;

    public BehaviorBatAttack(EnemyBase enemy)
    {
        this.enemy = enemy;
    }

    public override NodeState Evaluate()
    {
        //goes after the player but if he miss he just keep going down and leaves the place.
        //it drops 

        if (!isInit)
        {
            //get the target.
            isInit = true;
            targetDir = GetDir();
            originalPos = enemy.transform.position;
        }


   

        enemy.AttackAnimation();
        enemy.MoveRb(targetDir);

        //after long eneough i will simply destroy this fella.

        if(Vector3.Distance(originalPos, enemy.transform.position) > 50)
        {
            enemy.TakeDamage(1000);
        }


        return NodeState.Running;
    }

    Vector3 GetDir()
    {
        return PlayerHandler.instance.transform.position - enemy.transform.position;
    }


}
