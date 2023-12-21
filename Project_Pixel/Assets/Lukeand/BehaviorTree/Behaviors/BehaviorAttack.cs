using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BehaviorAttack : Node
{
    EnemyBase enemy;
    

    bool isInit;
    float currentCooldown;
    float totalCooldown;
    int animationTrackIndex;

    float current;
    float total;


    public BehaviorAttack(EnemyBase enemy, int animationTrackIndex)
    {
        this.enemy = enemy;
        this.animationTrackIndex = animationTrackIndex;

        total = 0.3f;
    }

    public override NodeState Evaluate()
    {
        //while its attacking we waiting.
        //rotate to thee right side?

        if (!isInit)
        {
            //enemy.DoAudioClip(animationTrackIndex);
            current = 0;
            isInit = true;
            enemy.CompleteStop();
            enemy.AttackAnimation();
        }      

        if(total > current)
        {
            current += Time.deltaTime;
            return NodeState.Running;
        }


        if (enemy.IsAttacking())
        {

            return NodeState.Running;
        }

        
        isInit = false;
        
        return NodeState.Success;
    }
}
