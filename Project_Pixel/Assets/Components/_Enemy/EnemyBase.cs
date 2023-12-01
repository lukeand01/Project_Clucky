using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyBase : Tree, IDamageable
{
    public string enemyName;
    [Separator("STATS")]
    public float damage;
    public float initialHealth;
    public float detectRange;
    public float moveSpeed;
    public float totalCooldown;
    public float patrolDistance;

    [Separator("PIECES")]
    public BoxCollider2D feetCollider;

    float currentHealth;

    bool isDead;
    bool isHit;

    GameObject graphicHolder;
    Animator anim;
    SpriteRenderer rend;
    Rigidbody2D rb;

    const string ANIMATION_IDLE = "Animation_Idle_";
    const string ANIMATION_WALK = "Animation_Walk_";
    const string ANIMATION_HIT = "Animation_Hit_";
    const string ANIMATION_DIE = "Animation_Die_";
    const string ANIMATION_ATTACK = "Animation_Attack_";
    const string ANIMATION_READY = "Animation_Ready_";


    [HideInInspector] public Vector3 originalPos;

    [Separator("Audio Clips")]
    [SerializeField] protected List<AudioClip> clipList = new();

    public bool shouldRotateJustGraphic;

    private void Awake()
    {
        currentHealth = initialHealth;
        SetUpComponents();
    }

    private void Start()
    {
        SetOriginalPos();
        SetUpBehavior();
    }

    public void SetOriginalPos()
    {
        originalPos = transform.position;
    }

    public void ControlRBGravity(float value)
    {
        rb.gravityScale = value;
    }

    public void ControlRBBodyType(RigidbodyType2D bodyType)
    {
        rb.bodyType = bodyType;
    }

    void SetUpComponents()
    {
        graphicHolder = transform.GetChild(0).gameObject;
        anim = graphicHolder.GetComponent<Animator>();
        rend = graphicHolder.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void SetUpBehavior()
    {

    }

    protected override void UpdateFunction()
    {
        base.UpdateFunction();

        
        //control the falling.

    }

    public void ClampFallSpeed(float value)
    {
        if(value > rb.velocity.y)
        {
            rb.velocity = new Vector2(rb.velocity.x, value);
        }
    }

    public void MoveRb(Vector2 dir)
    {
        rb.velocity += dir * moveSpeed * Time.deltaTime;
    }
    public void ForceMoveRb(float dir)
    {
        rb.velocity = new Vector3(dir * moveSpeed, rb.velocity.y);
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(HitProcess());
        }
    }
    void Die()
    {

    }

    IEnumerator HitProcess()
    {
        isHit = true;
        HitAnimation();
        yield return new WaitForSeconds(0.5f);
        isHit = false;
    }


    public void CompleteStop()
    {
        rb.velocity = Vector2.zero;
    }


    public int currentDir { get; private set; }
    public void MoveHorizontal(int dir, float speedModifier = 1)
    {
        rb.velocity = new Vector2(dir * moveSpeed * speedModifier, rb.velocity.y);
        currentDir = dir;

        RotateSprite(dir);
    }
    public void RotateSprite(int dir)
    {
        if (dir == 0) return;


        if (shouldRotateJustGraphic)
        {
            if (dir == -1)
            {
                graphicHolder.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            if (dir == 1)
            {
                graphicHolder.transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        }
        else
        {
            if (dir == -1)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            if (dir == 1)
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        }

        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 3) return;

        //otherwise you deal damage to the player.
        PlayerResource resource = collision.gameObject.GetComponent<PlayerResource>();

        if (resource == null) return;

        resource.TakeDamage(damage);

    }

    #region GRAPHICS

    public void IdleAnimation()
    {
        if (IsAttacked()) return;
        if (IsAnimationRunning(ANIMATION_READY)) return;
        if (IsAttacking()) return;

        PlayAnimation(ANIMATION_IDLE);
    }
    public void WalkAnimation()
    {
        if (IsAttacked()) return;
        if (IsAnimationRunning(ANIMATION_READY)) return;
        if (IsAttacking()) return;

        PlayAnimation(ANIMATION_WALK);
    }
    public void AttackAnimation()
    {
        PlayAnimation(ANIMATION_ATTACK);
    }
    public void HitAnimation()
    {
        PlayAnimation(ANIMATION_HIT);
    }
    public void DieAnimation()
    {
        PlayAnimation(ANIMATION_DIE);
    }
    public void ReadyAnimation()
    {
        if (IsAnimationRunning(ANIMATION_READY)) return;
        PlayAnimation(ANIMATION_READY);
    }

    void PlayAnimation(string id)
    {
        anim.Play(id + enemyName);
    }

    public bool IsAttacked()
    {
        if (IsAnimationRunning(ANIMATION_HIT) && isHit) return true;

        return false;
    }

    public bool IsAttacking()
    {
        return IsAnimationRunning(ANIMATION_ATTACK);
    }
    
    bool IsAnimationRunning(string id)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(id + enemyName);
    }


    #endregion

    #region UTILS
    public bool IsWall(int dir, float distance)
    {
        bool check = Physics2D.CircleCast(transform.position,0.5f,  Vector2.right * dir, distance, LayerMask.GetMask("Ground"));

        return check;
    }

    public bool IsLedge(int dir, float distance)
    {
        bool check = Physics2D.Raycast(transform.position + (new Vector3(1f,0,0) * dir), Vector3.down, distance, LayerMask.GetMask("Ground"));


        return !check;
    }

    public int GetDirToPlayer()
    {
        Vector3 value = (PlayerHandler.instance.transform.position - transform.position);

        if(value.x >= 0)
        {
            return 1;
        }
        if(value.x < 0)
        {
            return -1;
        }

        return 0;


    }


    public bool IsGrounded(float modifier = 0)
    {
        return Physics2D.BoxCast(feetCollider.bounds.center, feetCollider.bounds.size, 0, Vector2.down, .1f + modifier, LayerMask.GetMask("Ground"));
    }


    public float GetDistanceToGround()
    {
       RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 999, LayerMask.GetMask("Ground"));
        return hit.distance;
    }

    public AudioClip GetAudioClip(int index)
    {
        if (index >= clipList.Count) return null;

        return clipList[index];


    }
    #endregion


    public virtual void ShootProjectil()
    {

    }
}
