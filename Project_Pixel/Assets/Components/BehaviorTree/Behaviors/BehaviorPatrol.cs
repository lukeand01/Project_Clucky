using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BehaviorPatrol : Node
{
    EnemyBase enemy;
    float patrolDistance = 0;
    float patrolWaitTime = 0;

    int currentDir = 1;
    bool alreadyChangeSide;


    float current;
    float total;


    public BehaviorPatrol(EnemyBase enemy, float patrolDistance, float patrolWaitTime)
    {
        this.enemy = enemy;



        this.patrolDistance = patrolDistance;
        this.patrolWaitTime = patrolWaitTime;

        total = 0.8f;
    }

    public override NodeState Evaluate()
    {
        Patrol();
        return NodeState.Success;
    }


    void Patrol()
    {

        alreadyChangeSide = false;
        enemy.MoveHorizontal(currentDir);
        enemy.WalkAnimation();

        if(current > 0)
        {
            current -= Time.deltaTime;
        }

        

        if (enemy.IsWall(currentDir, 0.55f) && current <= 0 || enemy.IsLedge(currentDir, 0.5f) && current <= 0)
        {

            alreadyChangeSide = true;
            currentDir *= -1;
            current = total;
        }

        if (Vector3.Distance(enemy.transform.position, enemy.originalPos) > patrolDistance && patrolDistance > 0 && !alreadyChangeSide && current <= 0)
        {

            currentDir *= -1;
            current = total;
        }


    }

    

}
