using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BehaviorDetect : Node
{
    //here we keep checking if there is someoenee being detected.
    //types of detection: can only detect above or below.
    EnemyBase enemy;
    public BehaviorDetect(EnemyBase enemy)
    {
        this.enemy = enemy;
    }


    //the bat and the bee wawnt to shoot down but to not care aabout things above.
    //cannot do anything when through wall.
    //it needs to shoot a raycast when is close enough.



    public override NodeState Evaluate()
    {
        if (IsDetectRange()) return NodeState.Success;
        else return NodeState.Failure;       
    }

   

    bool IsDetectRange()
    {
        Transform playerPos = PlayerHandler.instance.transform;
        float distance = Vector3.Distance(enemy.transform.position, playerPos.position);

        if (distance > enemy.detectRange)
        {
            return false;
        }

        Vector3 diffY = enemy.transform.position - PlayerHandler.instance.transform.position;
        

        if(diffY.y > 5)
        {
            return false;
        }

        bool check = Physics2D.Raycast(enemy.transform.position, (playerPos.transform.position - enemy.transform.position), int.MaxValue, 6);

        if (check)
        {
            return false;
        }

        return true;

    }
    
}
