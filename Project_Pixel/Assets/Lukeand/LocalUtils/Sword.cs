using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    //this deals damage but to the wielder.



    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer != 3) return;

        IDamageable damage = collision.gameObject.GetComponent<IDamageable>();

        if (damage == null) return;

        damage.TakeDamage(0);
    }
}
