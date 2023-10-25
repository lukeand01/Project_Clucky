using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    PlatformEffector2D effect;

    private void Awake()
    {
        effect = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if(PlayerHandler.instance== null)
        {
            return;
        }


        if (PlayerHandler.instance.controller.isAllowedDown)
        {
            //then we allow the thing to go down.
            effect.rotationalOffset = 180;
        }
        else
        {
            //we dont.
            effect.rotationalOffset = 0;
        }
    }


}
