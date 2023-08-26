using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour, IPickable
{
    public void Pick()
    {
        GameHandler.instance.WinGame();
        Destroy(gameObject);
    }
}
