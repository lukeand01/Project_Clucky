using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class BehaviorBossSlime : Node
{
    SlimeBoss slime;
    int currentDir;
    int currentBehavior;
    int lastBehavior;

    //this is the period it is acting. then it instantly change to a another one.
    float totalCooldownToShoot;
    float currentCooldownToShoot;

    float patrolDistance;
    bool alreadyChangedSide;
    float currentPatrolTurning;
    float totalPatrolTurning;

    bool alreadyChangeSide;

    bool isSetUp;

    public BehaviorBossSlime(SlimeBoss slime)
    {
        this.slime = slime;

        totalCooldownToShoot = 5;
        currentDir = -1;
        patrolDistance = slime.patrolDistance;
        totalPatrolTurning = 0.2f;
    }



    public override NodeState Evaluate()
    {
        //
        //checking if should change the behavior.
        //when changing we call animation.
        if (!slime.hasStarted)
        {
            return NodeState.Failure;
        }

              

        PatrollingMovement();
        HandleShooting();
        //move and check moving behavior.

        return base.Evaluate();
    }

    //constantly be shooting
    //and the ceilling constantly be shooting.

    void HandleShooting()
    {
        //shoot in 5 directions.
        //x = 1 && x = -1
        //x = 1 y = 0.5 && x = -1 y =0.5
        //y = 1
        
        if(totalCooldownToShoot > currentCooldownToShoot)
        {
            currentCooldownToShoot += Time.deltaTime;
        }
        else
        {
            slime.ShootProjectil(new Vector3(1,0,0));
            slime.ShootProjectil(new Vector3(-1, 0, 0));
            slime.ShootProjectil(new Vector3(1, 0.5f, 0));
            slime.ShootProjectil(new Vector3(-1, 0.5f, 0));
            slime.ShootProjectil(new Vector3(0, 1, 0));
            currentCooldownToShoot = 0;
        }

    }

    

    void PatrollingMovement()
    {
        //it walks from one side to another.

        slime.MoveHorizontal(currentDir);

        if(currentPatrolTurning > 0)
        {
            currentPatrolTurning -= Time.deltaTime;
        }

        if(slime.IsWall(currentDir, 3))
        {
            Debug.Log("wall");
        }

        if (slime.IsWall(currentDir, 3f) && currentPatrolTurning <= 0)
        {

            alreadyChangeSide = true;
            currentDir *= -1;
            currentPatrolTurning = totalPatrolTurning;
        }

        Debug.Log("original pos " + slime.originalPos + " - " + currentDir);
        if(Vector3.Distance(slime.transform.position, slime.originalPos) > patrolDistance)
        {
            Debug.Log("it should be turning");
        }

        if (Vector3.Distance(slime.transform.position, slime.originalPos) > patrolDistance && patrolDistance > 0 && !alreadyChangedSide && currentPatrolTurning <= 0)
        {
            Debug.Log("turn this");
            currentDir *= -1;
            currentPatrolTurning = totalPatrolTurning;
        }
    }

}


//shoot in 5 dir above
//then something else shoots 
//