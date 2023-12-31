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

        total = 0.2f;
    }

    public override NodeState Evaluate()
    {
        if (enemy.IsAttacking()) return NodeState.Failure;
        Patrol();
        return NodeState.Success;
    }


    void Patrol()
    {

        alreadyChangeSide = false;
        enemy.MoveHorizontal(currentDir);
        enemy.WalkAnimation();
        //turn it on
        if(enemy.IsGrounded()) 
        {
            enemy.ControlAudioSourcePitch();
            enemy.ControlAudioSource(true);
        }
        else
        {
            enemy.ControlAudioSource(false);
        }

        

        if (current > 0)
        {
            current -= Time.deltaTime;
        }

        if (enemy.IsWall(currentDir, 0.3f)) //this is just for testing
        {
            
        }
        if (enemy.IsLedge(currentDir, 1.5f))//this is just for testing
        {

        }

        if (enemy.IsWall(currentDir, 0.3f) && current <= 0 || enemy.IsLedge(currentDir, 1.5f) && current <= 0)
        {
            alreadyChangeSide = true;
            currentDir *= -1;
            current = total;
        }

        if (Vector3.Distance(enemy.transform.position, enemy.originalPos) > patrolDistance && patrolDistance > 0 && !alreadyChangeSide && current <= 0)
        {
            Debug.Log("too far");
            currentDir *= -1;
            current = total;
        }


    }

    

}
