using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] float jumpModifier;
    Animator anim;
    const string ACTIVATEANIMATION = "Animation_Jumper_Activate";
    private void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer != 3) return;

        

        PlayerHandler handler = collision.gameObject.GetComponent<PlayerHandler>();

        if (handler == null) return;

        

        if (handler.IsFalling())
        {
            //send the fella back.
            handler.move.JumperJump(jumpModifier);
            anim.Play(ACTIVATEANIMATION);
        }

    }

}
