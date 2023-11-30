using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [HideInInspector]public bool isFallen;


    public bool DEBUGisImmune;

    public int PlayerGold;

    [HideInInspector] public AudioSource walkSource;

    [Separator("SFX")]
    public AudioClip jumpSFX;
    public AudioClip fallSFX;
    public AudioClip deathSFX;
    public AudioClip walkSFX;

    [Separator("AUDIO")]
    public AudioClip deathBGM;

    #region GOLD
    //
    public void AddCoin(int coin)
    {
        PlayerGold += coin;
        GameHandler.instance.observer.OnMMUpdateGold(PlayerGold);
    }
    public void RemoveCoin(int coin)
    {
        PlayerGold -= coin;
        PlayerGold = Mathf.Clamp(PlayerGold, 0, 1000000);
        GameHandler.instance.observer.OnMMUpdateGold(PlayerGold);
    }

    public void SetCoin(int coin)
    {
        PlayerGold = coin;
        GameHandler.instance.observer.OnMMUpdateGold(PlayerGold);
    }

    public bool HasCoin(int value)
    {
        return PlayerGold >= value;
    }

    public void UpdateMMUI()
    {
        GameHandler.instance.observer.OnMMUpdateGold(PlayerGold);
    }
    #endregion

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

        totalPick = 0.05f;

        walkSource = GetComponent<AudioSource>();

        if (walkSource == null) walkSource = gameObject.AddComponent<AudioSource>();

        walkSource.clip = walkSFX;
        walkSource.loop = true;

    }

    public void ReceiveSaveData(SaveClass save)
    {      
        PlayerGold = save.playerGold;
        StartCoroutine(UpdateMainMenuProcess());
    }

    IEnumerator UpdateMainMenuProcess()
    {
        yield return new WaitForSeconds(0.01f);
        GameHandler.instance.observer.OnMMUpdateGold(PlayerGold);
    }


    private void Update()
    {
        if(currentPick > 0)
        {
            currentPick -= Time.deltaTime;
        }
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

    public void ControlGravity(float gravityValue, string DEBUGORDERLOCATION)
    {
        rb.gravityScale = gravityValue;
    }

    #endregion


    
    public void ResetPlayer()
    {
        block.ClearBlock();
        isFallen = false;
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
    public Vector2 GetGroundPoint()
    {
        return Physics2D.BoxCast(feetCollider.bounds.center, feetCollider.bounds.size, 0, Vector2.down, 10, jumpableLayer).point;
    }

    public bool IsFalling()
    {
        return rb.velocity.y < -1;
    }

    #endregion


    float currentPick;
    float totalPick = 0.05f;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (currentPick > 0)
        {
            return;
        }

        IPickable pick = collision.gameObject.GetComponent<IPickable>();

        if (pick == null) return;
        pick.Pick();
        currentPick = totalPick;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IPickable pick = collision.gameObject.GetComponent<IPickable>();

        if (pick == null) return;

        Debug.Log("yo");
        pick.Pick();
    }
}

//what was the collision about?