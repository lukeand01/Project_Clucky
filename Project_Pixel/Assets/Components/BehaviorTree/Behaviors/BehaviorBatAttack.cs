using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorBatAttack : Node
{
    //
    EnemyBase enemy;

    public BehaviorBatAttack(EnemyBase enemy)
    {
        this.enemy = enemy;
    }

    public override NodeState Evaluate()
    {
        //goes after the player but if he miss he just keep going down and leaves the place.
        //it drops 

        



        return NodeState.Success;
    }



}
