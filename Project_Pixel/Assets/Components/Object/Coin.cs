using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IPickable
{
    //there are two values:
    //local and total

    string id;
    int index;
    GameObject holder;

    private void Awake()
    {
        id = Guid.NewGuid().ToString();
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }


    public void Pick()
    {

        //maybe the problem is having two placees to add coin.
        LocalHandler.instance.AddLocalGold(index);
        Destroy(gameObject);
    }

    public string GetID()
    {
        return id;
    }
}
