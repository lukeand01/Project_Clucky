using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBee : EnemyBase
{
    [Separator("BEE")]
    [SerializeField] BeeBullet beeProjectil;
    [SerializeField] Transform beeShootingPos;
    [SerializeField] float projectilSpeed;

    protected override void SetUpBehavior()
    {
        base.SetUpBehavior();
        UpdateTree(GetBehavior());
    }

    protected override void UpdateFunction()
    {
        base.UpdateFunction();

    }

    Sequence2 GetBehavior()
    {
        return new Sequence2(new List<Node>
        {
            new BehaviorDetect(this, false),
            new BehaviorReady(this, 0.5f),
            new BehaviorBeeAttack(this, totalCooldown)

        });
    }


    public override void ShootProjectil()
    {
        BeeBullet bullet = Instantiate(beeProjectil, beeShootingPos.position, Quaternion.identity);     
        bullet.SetUp(GetDir(), projectilSpeed);
    }

    Vector3 GetDir()
    {
        Vector3 dir = (PlayerHandler.instance.transform.position - transform.position).normalized;
        return dir;
    }
}
