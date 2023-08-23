using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public static PlayerHandler instance;

    [HideInInspector] public PlayerMove move;
    [HideInInspector] public PlayerController controller;
    [HideInInspector] public PlayerGraphic graphic;
    [HideInInspector] public PlayerCamera cam;
    [HideInInspector] public PlayerResource resource;

    [HideInInspector]public GameObject body; //graphic holder.
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [SerializeField] BoxCollider2D feetCollider;

    [Separator("LAYERMASKS")]
    [SerializeField] LayerMask jumpableLayer;


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        SetUpPlayerScripts();
        SetUpComponents();
    }

    #region SETUP
    void SetUpPlayerScripts()
    {
        move = GetComponent<PlayerMove>();
        controller = GetComponent<PlayerController>();
        graphic = GetComponent<PlayerGraphic>();
        cam = GetComponent<PlayerCamera>();
        resource = GetComponent<PlayerResource>();
    }
    void SetUpComponents()
    {

        body = transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody2D>();
        anim = body.GetComponent<Animator>();
    }
    #endregion

    #region BODY CONTROL

    public void ControlGravity(float gravityValue)
    {
        rb.gravityScale = gravityValue;
    }

    #endregion


    #region UTILS
    public bool IsGrounded()
    {
        return Physics2D.BoxCast(feetCollider.bounds.center, feetCollider.bounds.size, 0, Vector2.down, .1f, jumpableLayer);
    }
    public bool IsCloseToGround(float distance)
    {
        return Physics2D.BoxCast(feetCollider.bounds.center, feetCollider.bounds.size, 0, Vector2.down, distance, jumpableLayer);
    }

    public bool IsFalling()
    {
        return rb.velocity.y < 0;
    }

    #endregion
}
