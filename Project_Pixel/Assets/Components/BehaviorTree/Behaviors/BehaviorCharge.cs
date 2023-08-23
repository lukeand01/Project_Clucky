using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class BehaviorCharge : Node
{
    EnemyBase enemy;

    float total;
    float current;
    bool IsInit;
    int dir;
    float chargeSpeedModifier;


    public BehaviorCharge(EnemyBase enemy, float chargeDistance, float chargeSpeedModifier)
    {
        this.enemy = enemy;
        this.chargeSpeedModifier = chargeSpeedModifier;
        total = chargeDistance;
    }


    public override NodeState Evaluate()
    {
        //it kept moving forward till timeer is out or wall.
        //hitting player doese not stop it.


        if (!IsInit)
        {
            dir = enemy.GetDirToPlayer();
            IsInit = true;
        }

        if(total > current)
        {
            //if enemy is hit then we stop.
            current += Time.deltaTime;

            if (enemy.IsAttacked())
            {
                current = 0;
                return NodeState.Failure;
            }

            if(enemy.IsWall(dir, 0.5f))
            {
                Debug.Log("there is a wall ahead");
                return NodeState.Success;
            }

            //play charge.
            enemy.AttackAnimation();
            enemy.MoveHorizontal(dir, 2.5f);
                      
            return NodeState.Running;
        }
        else
        {
            IsInit = false;
            current = 0;
            enemy.SetOriginalPos();
            return NodeState.Success;
        }

      
    }


}
