using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    //if this object touches a player deals dadmage.



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Player") return;

        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable == null) return;

        damageable.TakeDamage(999);

    }
}
