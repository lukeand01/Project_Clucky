using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        IDamageable damage = collision.gameObject.GetComponent<IDamageable>();
        if (damage == null) return;

        if(collision.GetComponent<PlayerHandler>() != null)
        {
            PlayerHandler.instance.isFallen = true;
            PlayerHandler.instance.block.AddBlock("Fallen", BlockClass.BlockType.Complete);
            StartCoroutine(Process(damage));
        }
        else
        {
            damage.TakeDamage(999);
        }

        
       
    }

    IEnumerator Process(IDamageable damageble)
    {
        yield return new WaitForSeconds(0.8f);
        damageble.TakeDamage(999);
    }
   


}
