using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorLineOfSight : Node
{

    EnemyBase enemy;
    bool lineSightIsTowardsTarget;
    float distance;
    LayerMask layer;
    public BehaviorLineOfSight(EnemyBase enemy, float distance)
    {
        this.enemy = enemy;
        this.distance = distance;

        layer |= (1 << 3);
    }

    public override NodeState Evaluate()
    {
        int dir = enemy.currentDir; 

        bool spotted = Physics2D.Raycast(enemy.transform.position, Vector3.right * dir, distance, layer);


        if (spotted)
        {

            enemy.DoAudioClip(0);
            return NodeState.Success;
        }
        else
        {
            return NodeState.Failure;
        }

    }

}
