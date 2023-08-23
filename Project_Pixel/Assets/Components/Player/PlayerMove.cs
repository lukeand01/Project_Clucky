using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //this receives the control input from the player controller through the player handler
    //this controls movement and jump.

    PlayerHandler handler;

    private void Awake()
    {
        handler = GetComponent<PlayerHandler>();


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

    public void MoveHorizontal(int dir)
    {

        if(dir == 0)
        {
            handler.graphic.IdleAnimation();
        }
        else
        {
            handler.graphic.RotateSprite(dir);
            handler.graphic.WalkAnimation();
            lastDir = dir;
        }

        currentDir = dir;
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

    bool mustJump;

    void HandleJump()
    {
        if (isApex)
        {
            handler.ControlGravity(apexGravity);

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

        if (!isGrounded && isReleased && !isApex)
        {
            handler.ControlGravity(fallingGravity);
        }

        if (isGrounded)
        {
            coyoteCurrent = 0;          
            cannotJump = false;
            speedModifierApex = 1;

            handler.ControlGravity(baseGravity);

            if (mustJump)
            {
                //
                PressJump(true);
            }

           

        }

        HandleLedges();

        if (!isGrounded && !cannotJump)
        {
            if(coyoteTotal > coyoteCurrent)
            {
                coyoteCurrent += Time.deltaTime;
            }
            else
            {
                cannotJump = true;
            }
        }

        

        if (handler.rb.velocity.y < 0)
        {
            


        }
    }

    void HandleLedges()
    {
        //whn we hit forwawrd in edges.
        //check if it collides "down"body , and upperbody is not colliding.
        
        if (isGrounded) return;

        bool hitTop = Physics2D.CircleCast(headCollider.position, 0.5f * lastDir, Vector2.up, 5, 6);
        Debug.Log("not grounded");
        if (hitTop) return;

        Debug.Log("nothing top");

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
        handler.ControlGravity(baseGravity);
        Jump(jumpForce);


        cooldownBeforeHoldCurrent = 0;
        jumpHeightCurrent = 0;
    }
    public void HoldJump()
    {
        if (isReleased) return;

        if(cooldownBeforeHoldCurrent < cooldownBeforeHoldTotal)
        {
            cooldownBeforeHoldCurrent += Time.deltaTime;
            return;
        }

       


        if(jumpHeightCurrent < jumpHeightTotal)
        {
            jumpHeightCurrent += Time.deltaTime;
            handler.ControlGravity(holdGravity);
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
        handler.ControlGravity(fallingGravity);
    }

    public void JumperJump(float modifier)
    {
        Jump(jumpForce * modifier);

        mustJump = false;
        isReleased = false;
        cannotJump = true;
        handler.ControlGravity(baseGravity);

        cooldownBeforeHoldCurrent = 0;
        jumpHeightCurrent = 0;
    }

    public void Jump(float jumpForce)
    {
        handler.rb.velocity = new Vector2(handler.rb.velocity.x, 0);

        Vector2 force = Vector2.up * jumpForce * -Physics2D.gravity.y * 0.02f;
        handler.rb.velocity += force;
    }
    #endregion

}
