using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class BehaviorBatAttack : Node
{
    //
    EnemyBase enemy;
    bool isInit;

    int dir;

    Vector3 originalPos;

    //drop timer.

    float currentDrop;
    float totalDrop;

    bool isNext;

    //wee clamp it.
    float currentFallSpeed;


    public BehaviorBatAttack(EnemyBase enemy, float totalDrop)
    {
        this.enemy = enemy;
        this.totalDrop = totalDrop;

        currentFallSpeed = -5;
    }

    public override NodeState Evaluate()
    {
        //goes after the player but if he miss he just keep going down and leaves the place.
        //it drops 
        enemy.AttackAnimation();

       
        if (!isNext)
        {
            if (enemy.IsGrounded(totalDrop))
            {
                isNext = true;
            }
            else
            {
                enemy.ControlRBBodyType(RigidbodyType2D.Dynamic);
            }

            return NodeState.Running;
        }

        
     
        if (!isInit)
        {
            enemy.ControlRBBodyType(RigidbodyType2D.Kinematic);
            
            dir = GetDir();
            isInit = true;
        }



        currentFallSpeed += Time.deltaTime * 15f;
        currentFallSpeed = Mathf.Clamp(currentFallSpeed, -9, -0.35f);

        enemy.ClampFallSpeed(currentFallSpeed);
        enemy.ForceMoveRb(dir);

        if (Vector3.Distance(originalPos, enemy.transform.position) > 50)
        {
            enemy.TakeDamage(1000);
        }


        return NodeState.Running;
    }

    int GetDir()
    {
        Vector3 diff = PlayerHandler.instance.transform.position - enemy.transform.position;

        if(diff.x > 0)
        {
            return 1;

        }

        if(diff.x < 0)
        {
            return -1;
        }

        return 1;


    }


}
