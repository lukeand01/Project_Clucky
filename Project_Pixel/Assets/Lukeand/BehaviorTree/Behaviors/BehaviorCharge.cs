using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;


public class BehaviorCharge : Node
{
    EnemyBase enemy;

    float total;
    float current;
    bool IsInit;
    int dir;
    float chargeSpeedModifier;
    LayerMask layer;

    public BehaviorCharge(EnemyBase enemy, float chargeDistance, float chargeSpeedModifier)
    {
        this.enemy = enemy;
        this.chargeSpeedModifier = chargeSpeedModifier;
        total = chargeDistance;

        layer |= (1 << 3);
    }


    public override NodeState Evaluate()
    {
        //it kept moving forward till timeer is out or wall.
        //hitting player doese not stop it.

        if (enemy.IsGrounded())
        {
            enemy.ControlAudioSourcePitch(1.4f);
            enemy.ControlAudioSource(true);
        }
        else
        {
            enemy.ControlAudioSource(false);
        }

        if (!IsInit)
        {
            
            dir = enemy.GetDirToPlayer();
            IsInit = true;
        }

        if(total > current)
        {
            //if enemy is hit then we stop.




            if (IsAhead(10))
            {
                
            }
            else
            {

                current += Time.deltaTime;
            }


            if (enemy.IsAttacked())
            {
                current = 0;
                IsInit = false;
                return NodeState.Failure;
            }

            
            

            if(enemy.IsWall(dir, 0.5f))
            {

                IsInit = false;
                return NodeState.Success;
            }

            //play charge.
            enemy.AttackAnimation();
            enemy.MoveHorizontal(dir, 2.5f);
                      
            return NodeState.Running;
        }
        else
        {
            Debug.Log("stopped by this");
            IsInit = false;
            current = 0;
            enemy.SetOriginalPos();
            return NodeState.Success;
        }
    }

    bool IsAhead(float distance)
    {
        return Physics2D.Raycast(enemy.transform.position, Vector3.right * dir, distance, layer);
    }
}
