using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search.Providers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerHandler handler;

    //this takes any input the player wants to use.
    [Separator("ESSENTIAL COMPONENTS")]
    [SerializeField] InputButton jumpInputButton;
    [SerializeField] InputButton leftInputButton;
    [SerializeField] InputButton rightInputButton;


    private void Awake()
    {
        handler = GetComponent<PlayerHandler>();

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
        if (jumpInputButton.value == 1) handler.move.HoldJump();      
    }

    void PressJumpInput()
    {
        //pressed it.
        handler.move.PressJump();
    }

    void ReleaseJumpInput()
    {
        handler.move.ReleaseJump();
    }
}

