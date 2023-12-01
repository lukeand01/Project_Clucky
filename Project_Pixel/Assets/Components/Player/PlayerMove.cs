using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //this receives the control input from the player controller through the player handler
    //this controls movement and jump.

    PlayerHandler handler;

    [Separator("PS REFERENCES")]
    [SerializeField] ParticleSystem psWalk;
    [SerializeField] ParticleSystem psJumpFall;
    [SerializeField] float totalForPSJumpFall;
    float currentForPSJumpFall;

    private void Awake()
    {
        handler = GetComponent<PlayerHandler>();

        totalCannotControl = 0.2f;
    }

    private void Update()
    {
        isGrounded = handler.IsGrounded();

        HandleJump();

        


    }


    #region MOVEMENT
    [Separator("MOVEMENT")]
    [SerializeField] float currentSpeed;
    float speedModifierApex = 1;

    int currentDir;
    int lastDir;

    public void MoveHorizontal(float dir)
    {

        float actualValue = Mathf.Abs(dir);
        int turnDir = 0;
        if(dir > 0)
        {
            actualValue = Mathf.Clamp(actualValue, 0.7f, 1);
            turnDir = 1;
        }
        else
        {
            actualValue = Mathf.Clamp(actualValue, 0.7f, 1);
            actualValue *= -1;
            turnDir = -1;
        }

        handler.walkSource.enabled = dir != 0 && isGrounded;
        psWalk.gameObject.SetActive(dir != 0 && isGrounded);

        if(dir == 0)
        {
            handler.graphic.IdleAnimation();
        }
        else
        {
            handler.graphic.RotateSprite(turnDir);
            handler.graphic.WalkAnimation();


            lastDir = turnDir;
        }

        currentDir = turnDir;
        handler.rb.velocity = new Vector2(dir * currentSpeed * speedModifierApex, handler.rb.velocity.y);
    }

    #endregion

    #region JUMP

    [Separator("JUMP")]
    [SerializeField] float jumpForce;

    [Separator("GRAVITY")]
    [SerializeField] float baseGravity;
    [SerializeField] float holdGravity;
    [SerializeField] float fallingGravity;

    bool isGrounded;
    bool cannotJump;
    bool isReleased;

    [Separator("HEIGHT")]
    [SerializeField] float cooldownBeforeHoldTotal = 0.2f;
    [SerializeField]float jumpHeightTotal;
    float cooldownBeforeHoldCurrent;
    float jumpHeightCurrent;   

    [Separator("COYOTE - no longer touching the ground")]
    [SerializeField] float coyoteTotal;
    float coyoteCurrent;

    [Separator("APEX TIME - additional momemtum when it reaches peak jump")]
    [SerializeField] float apexGravity;
    [SerializeField] float apexTotal;
    float apexCurrent; 
    bool isApex;

    [Separator("JUMP BUFFERING - when you press input before being grounded")]
    [SerializeField] float distanceToGround;

    [Separator("MAX FALL SPEED")]
    [SerializeField] float maxFallSpeed;

    [Separator("COLLIDERS")]
    [SerializeField] Transform headCollider;
    [SerializeField] Transform bottomCollider;
    //its a moment. if the jump is bigger nough.

    [Separator("DOUBLE JUMP")]
    [SerializeField] int totalDoubleJump;
    int currentDoubleJumps;

    bool cannotControl;

    float currentCannotControl;
    float totalCannotControl = 0.15f;

    bool mustJump;

    void HandleJump()
    {
        if(currentForPSJumpFall > 0)
        {
            currentForPSJumpFall -= Time.deltaTime;
        }


        if(cannotControl && currentCannotControl > 0)
        {
            currentCannotControl -= Time.deltaTime;
            Debug.Log("stuck here");
            return;
        }
                   

        if (cannotControl)
        {
            if (isGrounded)
            {
                cannotControl = false;

            }
            else
            {
                return;
            }
            
        }

        if (isApex)
        {
            handler.ControlGravity(apexGravity, "Apex");

            if(apexTotal > apexCurrent)
            {
                apexCurrent += Time.deltaTime;
            }
            else
            {
                apexCurrent = 0;
                isApex = false;

                
            }
        }



        if (!isGrounded)
        {
            if(isReleased && !isApex) handler.ControlGravity(fallingGravity, "falling");

            if (!cannotJump)
            {
                if (coyoteTotal > coyoteCurrent)
                {
                    coyoteCurrent += Time.deltaTime;
                }
                else
                {
                    cannotJump = true;
                }
            }

            if (handler.IsCloseToGround(0.1f) && handler.rb.velocity.y < 5)
            {
                SpawnPSJump();

            }

        }


        if (isGrounded)
        {
            currentDoubleJumps = 0;
            coyoteCurrent = 0;          
            cannotJump = false;
            speedModifierApex = 1;

            handler.ControlGravity(baseGravity, "base grounded");

            if (mustJump)
            {
                //
                PressJump(true);
            }

           

        }

        HandleLedges();

        

        

        
    }

    void HandleLedges()
    {
        //whn we hit forwawrd in edges.
        //check if it collides "down"body , and upperbody is not colliding.
        
        if (isGrounded) return;

        bool hitTop = Physics2D.CircleCast(headCollider.position, 0.5f * lastDir, Vector2.up, 5, 6);
        if (hitTop) return;


        bool hitBottom = Physics2D.CircleCast(bottomCollider.position, 0.5f * lastDir, Vector2.up, 5, 6);

        if (hitBottom)
        {
            //if this happens then we give a push forward.
            handler.rb.AddForce(new Vector2(2 * lastDir, 3), ForceMode2D.Impulse);
            Debug.Log("got to the end");
        }

        


    }

    public void PressJump(bool cannotForceJump = false)
    {
        //do nothing if holding down.
        if (cannotControl) return;
        if (!isGrounded && currentDoubleJumps < totalDoubleJump)
        {
            currentDoubleJumps++;
            mustJump = false;
            isReleased = false;
            cannotJump = true;
            handler.ControlGravity(baseGravity, "preess");
            Jump(jumpForce);
            GameHandler.instance.sound.CreateSFX(handler.jumpSFX);

            cooldownBeforeHoldCurrent = 0;
            jumpHeightCurrent = 0;
            return;
        }


        if (cannotJump)
        {
            if (!cannotForceJump && handler.IsCloseToGround(distanceToGround))
            {
                //leave it an order.
                Debug.Log("leave an order");
                mustJump = true;
                
            }
            return;
        }

        mustJump = false;
        isReleased = false;
        cannotJump = true;
        handler.ControlGravity(baseGravity, "base");
        Jump(jumpForce);

        if(handler.jumpSFX != null && GameHandler.instance != null) GameHandler.instance.sound.CreateSFX(handler.jumpSFX);
        //spawn the thing by the base.
        if (isGrounded) SpawnPSJump();

        cooldownBeforeHoldCurrent = 0;
        jumpHeightCurrent = 0;
    }
    public void HoldJump()
    {
        if (isReleased) return;
        if (cannotControl) return;

        if (cooldownBeforeHoldCurrent < cooldownBeforeHoldTotal)
        {
            cooldownBeforeHoldCurrent += Time.deltaTime;
            return;
        }



        if(jumpHeightCurrent < jumpHeightTotal)
        {
            jumpHeightCurrent += Time.deltaTime;
            handler.ControlGravity(holdGravity, "hold");
            Jump(jumpForce);
        }
        else
        {
            isApex = true;
            Jump(jumpForce / 2.5f);
            ReleaseJump();
        }
        
    }

    public void ReleaseJump()
    {
        isReleased = true;
        handler.ControlGravity(fallingGravity, "Falling release");
    }

    public void JumperJump(float modifier)
    {
        PlayerHandler.instance.cam.ForceFollow();

        Jump(jumpForce * modifier);
        GameHandler.instance.sound.CreateSFX(handler.jumpSFX);
        cannotControl = true;
        currentCannotControl = totalCannotControl;

        mustJump = false;
        isReleased = true;
        cannotJump = true;
        handler.ControlGravity(baseGravity * 1.3f, "jumper jump");

        cooldownBeforeHoldCurrent = 0;
        jumpHeightCurrent = 0;
    }

    public void Jump(float jumpForce)
    {
        handler.rb.velocity = new Vector2(handler.rb.velocity.x, 0);

        Vector2 force = Vector2.up * jumpForce * -Physics2D.gravity.y * 0.02f;
        handler.rb.velocity += force;
    }

    void SpawnPSJump()
    {
        //we need to get information relating to the ground.

        if (currentForPSJumpFall > 0)
        {
            Debug.Log("couldnt here");
            return;
        }

        currentForPSJumpFall = totalForPSJumpFall;

        ParticleSystem newObject = Instantiate(psJumpFall, handler.GetGroundPoint(), Quaternion.identity);
        newObject.gameObject.SetActive(true);
        newObject.gameObject.GetComponent<DestroySelf>().SetUp(newObject.main.duration + 0.1f);
    }

    #endregion


    //

}
