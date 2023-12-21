using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : EnemyBase
{
    //actually it can be just a normal creature.


    public bool hasStarted;

    [SerializeField] ProjectilBase slimeProjectil;

    [Separator("SLIME DEBUG SPRITES")]
    [SerializeField] Sprite sleepingSprite;
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite axeSprite;
    [SerializeField] Sprite starSprite;
    [SerializeField] Sprite explosionSprite;

    public void StartSlime()
    {
        hasStarted = true;
    }

    protected override void SetUpBehavior()
    {
        base.SetUpBehavior();

        UpdateTree(GetSlimeBehavior());
    }

    Sequence2 GetSlimeBehavior()
    {
        return new Sequence2(new List<Node>
        {
            new BehaviorBossSlime(this)
        });
    }

    public void ShootProjectil(Vector3 dir)
    {
        ProjectilBase project = Instantiate(slimeProjectil, transform.position, Quaternion.identity);
        project.SetUp(dir,1.4f);     
    }


}
