using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IPickable
{
    //there are two values:
    //local and total


    public void Pick()
    {
        Debug.Log("coin");
        LocalHandler.instance.AddLocalGold();
        PlayerHandler.instance.AddCoin();
        Destroy(gameObject);
    }
}
