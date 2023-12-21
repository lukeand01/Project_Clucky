using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlimeStageHandler : MonoBehaviour
{
    //this takes the info from the number of buttons pressed and then relay the info 

    [SerializeField] ChangeTerrainVariables[] terrainsToFall;

   
    public void StartFalling()
    {
        StartCoroutine(MakeTerrainCollapse());
    }

    IEnumerator MakeTerrainCollapse()
    {
        Debug.Log("this was called");

       

        for (int i = terrainsToFall.Length - 1; i > 0; i--)
        {
            terrainsToFall[i].ActivateGravity();
            yield return new WaitForSeconds(0.4f);
        }
    }

    
}
