using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraphic : MonoBehaviour
{
    PlayerHandler handler;

    const string ID = "Animation_Chicken_";
    const string IDLE = "Idle";
    const string WALK = "Walk";

    private void Awake()
    {
        handler = GetComponent<PlayerHandler>();
    }

    public void RotateSprite(int dir)
    {
        if (dir == 0) return;
        //rotate to where you are facing.
        if (dir == 1)
        {
            //handler.graphicHolder.transform.localPosition = new Vector3(0, 0, 0);
            handler.body.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (dir == -1)
        {
            handler.body.transform.rotation = new Quaternion(0, 180, 0, 0);
            //handler.body.transform.localPosition = new Vector3(-0.4f, 0, 0);
        }
    }


    public void IdleAnimation()
    {
        handler.anim.Play(ID + IDLE);
    }

    public void WalkAnimation()
    {
        handler.anim.Play(ID + WALK);
    }
}
