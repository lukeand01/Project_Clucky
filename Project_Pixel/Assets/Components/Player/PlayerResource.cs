using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResource : MonoBehaviour, IDamageable
{
    [SerializeField] float initialHealth;
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;

    bool isDead;

    private void Awake()
    {
        maxHealth = initialHealth;
        currentHealth = maxHealth;
    }


    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
        else
        {

        }
    }

    IEnumerator DamageProcess()
    {
        yield return null;
    }



    void Die()
    {
        isDead = true;

        //trigger death animation
        //trigger death ui.
    }
}
