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
    [HideInInspector] public BoxCollider2D boxCollider;
    [HideInInspector] public CapsuleCollider2D capsuleCollider;
    [SerializeField] BoxCollider2D feetCollider;

    [HideInInspector]public BlockClass block;


    [Separator("LAYERMASKS")]
    [SerializeField] LayerMask jumpableLayer;


    public int coinTotal;
    public void AddCoin()
    {
        
        coinTotal += 1;

    }

    [ContextMenu("SDASD")]
    public void STUFF()
    {
        Time.timeScale = 0.1f;
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        SetUpPlayerScripts();
        SetUpComponents();

        block = new BlockClass();
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
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    #endregion

    #region BODY CONTROL

    public void ControlGravity(float gravityValue)
    {
        rb.gravityScale = gravityValue;
    }

    #endregion

    public void ResetPlayer()
    {

        resource.ResetResource();
    }


    public void FreezeRB(bool choice)
    {
        if (choice)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

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



    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickable pick = collision.gameObject.GetComponent<IPickable>();

        if (pick == null) return;
        pick.Pick();
    }
}
