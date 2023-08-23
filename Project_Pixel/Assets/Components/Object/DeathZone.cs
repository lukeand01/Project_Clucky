using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damage = collision.gameObject.GetComponent<IDamageable>();

        if (damage == null) return;

        damage.TakeDamage(999);

    }


}
