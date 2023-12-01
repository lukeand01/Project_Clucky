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
    }

    public override NodeState Evaluate()
    {
        int dir = enemy.currentDir; 

        bool spotted = Physics2D.Raycast(enemy.transform.position, Vector3.right * dir, distance, layer);
        Debug.Log("spotteed " + spotted);

        if (spotted)
        {
            return NodeState.Success;
        }
        else
        {
            return NodeState.Failure;
        }

    }

}
