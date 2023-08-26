using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerHandler handler;

    //this takes any input the player wants to use.

    public InputButton jumpInputButton;
    public InputButton leftInputButton;
    public InputButton rightInputButton;

    private void Awake()
    {
        handler = GetComponent<PlayerHandler>();

        
    }

    private void Start()
    {
        jumpInputButton = ButtonInputHandler.instance.jumpInputButton;
        leftInputButton = ButtonInputHandler.instance.leftInputButton;
        rightInputButton = ButtonInputHandler.instance.rightInputButton;


        jumpInputButton.EventPressed += PressJumpInput;
        jumpInputButton.EventReleased += ReleaseJumpInput;
    }

    private void Update()
    {

        InputMove();
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

    void HoldJumpInput()
    {
        if (handler.block.HasBlock(BlockClass.BlockType.Partial)) return;
        if (jumpInputButton.value == 1) handler.move.HoldJump();      
    }

    void PressJumpInput()
    {
        //pressed it.
        if (handler.block.HasBlock(BlockClass.BlockType.Partial)) return;
        handler.move.PressJump();
    }

    void ReleaseJumpInput()
    {
        if (handler.block.HasBlock(BlockClass.BlockType.Partial)) return;
        handler.move.ReleaseJump();
    }
}

