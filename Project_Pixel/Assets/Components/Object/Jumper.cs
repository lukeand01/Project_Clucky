using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] float jumpModifier;
    Animator anim;
    const string ACTIVATEANIMATION = "Animation_Jumper_Activate";

    float total = 0.15f;
    float current;


    private void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }


    private void Update()
    {
        if (current > 0) current -= Time.deltaTime;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (current > 0) return;

        if (collision.gameObject.layer != 3) return;
       
        PlayerHandler handler = collision.gameObject.GetComponent<PlayerHandler>();

        if (handler == null) return;

        current = total;
        handler.move.JumperJump(jumpModifier);
        anim.Play(ACTIVATEANIMATION);

        

    }

}
