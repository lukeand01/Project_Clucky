using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
        currentCooldown = totalCooldown * 0.2f;
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

        enemy.DoAudioClip(0);
        enemy.ShootProjectil();

        

        currentCooldown = UnityEngine.Random.Range(totalCooldown * 0.7f, totalCooldown * 1.2f); ;
        return NodeState.Success;
    }

    

}
