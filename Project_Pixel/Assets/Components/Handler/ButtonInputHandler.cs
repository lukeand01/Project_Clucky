using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInputHandler : MonoBehaviour
{
    public void Pause()
    {
        UIHolder.instance.pause.Control();
    }
}
