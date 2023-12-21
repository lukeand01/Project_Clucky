using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTerrainVariables : MonoBehaviour
{
    //this exists just because im lazy to change the stuff.

    SpriteRenderer[] rends;
    Rigidbody2D rb;


    public Color rendColor;

    private void Awake()
    {
        rends = transform.GetComponentsInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();



        foreach (var item in rends)
        {
            item.color = rendColor;
        }

    }

    public void ActivateGravity()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.constraints = RigidbodyConstraints2D.None;
    }
    

}
