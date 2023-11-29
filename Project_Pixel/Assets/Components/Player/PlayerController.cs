using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerHandler handler;

    //this takes any input the player wants to use.

    [HideInInspector] public InputButton jumpInputButton;
    [HideInInspector] public InputButton leftInputButton;
    [HideInInspector] public InputButton rightInputButton;
    [HideInInspector] public InputButton downInputButton;

    FixedJoystick joystick;


    public bool isHoldingDown;
    public bool isHoldingUp;
    public bool isAllowedDown;

    private void Awake()
    {
        handler = GetComponent<PlayerHandler>();

        
    }

    private void Start()
    {
        jumpInputButton = ButtonInputHandler.instance.jumpInputButton;
        leftInputButton = ButtonInputHandler.instance.leftInputButton;
        rightInputButton = ButtonInputHandler.instance.rightInputButton;
        downInputButton = ButtonInputHandler.instance.downInputButton;

        joystick = ButtonInputHandler.instance.joystick;


        jumpInputButton.EventPressed += PressJumpInput;
        jumpInputButton.EventReleased += ReleaseJumpInput;

    }

    private void Update()
    {

        //InputMove();
        InputMove2();
        HoldJumpInput();
    }


    //use a mobile controller
    //use a button for jumping.

    void InputMove()
    {
        if (handler.block.HasBlock(BlockClass.BlockType.Partial)) return;
        if (leftInputButton.value == 1)
        {
            handler.move.MoveHorizontal(-1);
            return;
        }

        if (rightInputButton.value == 1)
        {
            handler.move.MoveHorizontal(1);
            return;
        }

        

        if(leftInputButton.value == 0 && rightInputButton.value == 0)
        {
            handler.move.MoveHorizontal(0);
        }

    }

    void InputMove2()
    {
        //it checks the input.
        //if its only up then we look up.

        if (joystick == null) return;

        if (Input.GetKey(KeyCode.D))
        {
            handler.move.MoveHorizontal(1);
            return;
        }
        if (Input.GetKey(KeyCode.A))
        {
            handler.move.MoveHorizontal(-1);
            return;
        }

        InputLookUp(IsUp());
        InputLookDown(IsDown());        
        if (IsRight())
        {          
            handler.move.MoveHorizontal(joystick.Direction.x);
            return;
        }
        if (IsLeft())
        {
            handler.move.MoveHorizontal(joystick.Direction.x);
            return;
        }
        handler.move.MoveHorizontal(0);
    }

    #region DASDAS
    bool IsRight()
    {
        if (joystick.Direction.x > 0.15f && joystick.Direction.y < 0.6f || joystick.Direction.x > 0.15f && joystick.Direction.y > -0.6f) return true;
        return false;
    }

    bool IsLeft()
    {
        if (joystick.Direction.x < -0.15f && joystick.Direction.y < 0.6f || joystick.Direction.x < -0.15f && joystick.Direction.y > -0.6f) return true;
        return false;
    }

    bool IsUp()
    {
        if (joystick.Direction.y > 0.6f && joystick.Direction.x < 0.2f) return true;
        return false;
    }

    bool IsDown()
    {
        if (joystick.Direction.y < -0.6f && joystick.Direction.x < 0.2f) return true;
        return false;
    }
    #endregion


    void InputLookDown(bool choice)
    {
        isHoldingDown = choice && handler.IsGrounded();       
    }

    void InputLookUp(bool choice)
    {
        //put the camera up.
        isHoldingUp = choice && handler.IsGrounded();
    }

    void HoldJumpInput()
    {
        if (handler.block.HasBlock(BlockClass.BlockType.Partial)) return;
        if (jumpInputButton.value == 1) handler.move.HoldJump();      
    }

    void PressJumpInput()
    {
        //pressed it.
        if (handler.block.HasBlock(BlockClass.BlockType.Partial)) return;

        if (isHoldingDown)
        {
            //wee make it so the player can go through 
            StopAllCoroutines();
            StartCoroutine(CanGoDownProcess());
        }
        else
        {
            handler.move.PressJump();
        }

        
    }

    IEnumerator CanGoDownProcess()
    {
        isAllowedDown = true;
        yield return new WaitForSeconds(0.4f);
        isAllowedDown = false;
    }

    void ReleaseJumpInput()
    {
        if (handler.block.HasBlock(BlockClass.BlockType.Partial)) return;
        handler.move.ReleaseJump();
    }




}

