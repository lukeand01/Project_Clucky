using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Suriken : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //push the fella back.
        if (collision.gameObject.layer != 3) return;

        IDamageable damage = collision.gameObject.GetComponent<IDamageable>();

        if (damage == null) return;
        
        damage.TakeDamage(0);

    }

}
