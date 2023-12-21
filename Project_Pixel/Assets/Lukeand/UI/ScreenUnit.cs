using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenUnit : MonoBehaviour
{
    //this fella will do the effects.


    GameObject holder;

    private void Awake()
    {
        holder = transform.GetChild(0).gameObject;

        if(holder.name != "Holder")
        {
            Debug.Log("something wrong this screen has not found the right holder " + gameObject.name);

        }

    }

    public void Open()
    {

    }

    public void Close()
    {

    }


    IEnumerator OpenProcess()
    {
        yield return null;
    }

    IEnumerator CloseProcess()
    {
        yield break;
    }
}
