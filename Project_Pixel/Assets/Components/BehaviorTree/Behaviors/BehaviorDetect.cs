using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BehaviorDetect : Node
{
    //here we keep checking if there is someoenee being detected.
    //types of detection: can only detect above or below.
    EnemyBase enemy;
    bool careAboutDiff;

    LayerMask checkLayerMask;

    public BehaviorDetect(EnemyBase enemy,  bool careAboutDiff = true)
    {
        this.enemy = enemy;
        this.careAboutDiff = careAboutDiff;

        //this adds to the layer mask the number of player and wall
        checkLayerMask |= (1 << 6);
        checkLayerMask |= (1 << 3);

        
    }

    

    //the bat and the bee wawnt to shoot down but to not care aabout things above.
    //cannot do anything when through wall.
    //it needs to shoot a raycast when is close enough.


    //in sight
    public override NodeState Evaluate()
    {
        
        if (IsDetectRange(careAboutDiff))
        {
            //


            return NodeState.Success;
        }
        else
        {
            return NodeState.Failure;
        }    
    }

   

    bool IsDetectRange(bool caresAboutDif = true)
    {
        Transform playerPos = PlayerHandler.instance.transform;
        float distance = Vector3.Distance(enemy.transform.position, playerPos.position);

        if (distance > enemy.detectRange)
        {
            return false;
        }


        if (caresAboutDif)
        {
            Vector3 diffY = enemy.transform.position - PlayerHandler.instance.transform.position;
            if (diffY.y > 5)
            {
                return false;
            }
        }


        RaycastHit2D check = Physics2D.Raycast(enemy.transform.position, (playerPos.position - enemy.transform.position).normalized, enemy.detectRange, checkLayerMask);
        
        //the problem is itself.

        if(check.collider != null)
        {
            if (check.collider.gameObject.tag == "Player")
            {
                return true;
            }         
        }

        return false;

        
        

    }
    
}
