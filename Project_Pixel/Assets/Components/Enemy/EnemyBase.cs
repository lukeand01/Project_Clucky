using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyBase : Tree, IDamageable
{
    [Separator("STATS")]
    public float damage;
    public float initialHealth;
    public float detectRange;
    public float moveSpeed;
    public float totalCooldown;
    public float patrolDistance;

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

    public void MoveHorizontal(int dir, float speedModifier = 1)
    {


        rb.velocity = new Vector2(dir * moveSpeed * speedModifier, rb.velocity.y);
       

        RotateSprite(dir);
    }
    public void RotateSprite(int dir)
    {
        if (dir == 0) return;

        if (dir == 1)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (dir == -1)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 3) return;

        //otherwise you deal damage to the player.
        PlayerResource resource = collision.gameObject.GetComponent<PlayerResource>();

        if (resource == null) return;

        Debug.Log("touched the player");
        resource.TakeDamage(damage);

    }

    #region GRAPHICS

    public void IdleAnimation()
    {
        if (IsAttacked()) return;
        if (IsAnimationRunning(ANIMATION_READY)) return;

        PlayAnimation(ANIMATION_IDLE);
    }
    public void WalkAnimation()
    {
        if (IsAttacked()) return;
        if (IsAnimationRunning(ANIMATION_READY)) return;

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
        anim.Play(id + gameObject.name);
    }

    public bool IsAttacked()
    {
        if (IsAnimationRunning(ANIMATION_HIT) && isHit) return true;

        return false;
    }
    
    bool IsAnimationRunning(string id)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(id + gameObject.name);
    }


    #endregion

    #region UTILS
    public bool IsWall(int dir, float distance)
    {
        bool check = Physics2D.Raycast(transform.position, Vector2.right * dir, distance, LayerMask.GetMask("Ground"));

        return check;
    }

    public bool IsLedge(int dir, float distance)
    {
        bool check = Physics2D.Raycast(transform.position + (new Vector3(2,0,0) * dir), Vector3.down, distance, LayerMask.GetMask("Ground"));

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

    #endregion
}
