using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorBeeAttack : Node
{
    EnemyBase enemy;

    float totalCooldown;
    float currentCooldown;



    public BehaviorBeeAttack(EnemyBase enemy, float totalCooldown)
    {
        this.enemy = enemy;

        this.totalCooldown = totalCooldown;
        currentCooldown = totalCooldown;
    }

    //shoot at positions.
    //stand still.


    public override NodeState Evaluate()
    {
        //shoot at player position. then wait for cooldown.


        if(currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            return NodeState.Running;
        }


        enemy.ShootProjectil();
        currentCooldown = totalCooldown;
        return NodeState.Success;
    }

    

}
