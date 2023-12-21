using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherScript : MonoBehaviour
{
    //

    [SerializeField] float initialSpeed = 1;    
    [SerializeField] float topSpeed = 2;
    float currentSpeed;

    //it increases speed till top.
    float totalRange = 0;
    float currentRange;

    bool shouldStartMoving;

    public void StartCrusher()
    {
        shouldStartMoving = true;
        totalRange = 12.5f;
        currentSpeed = initialSpeed;
    }


    private void FixedUpdate()
    {
        //it goes down.
        if (!shouldStartMoving) return;


        currentRange += 0.01f;

        if(currentRange > totalRange)
        {
            shouldStartMoving = false;
            return;
        }


        currentSpeed += 0.01f;
        currentSpeed = Mathf.Clamp(currentSpeed, initialSpeed, topSpeed);
        transform.position += Vector3.down * 0.002f * currentSpeed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;



        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable == null) return;

        damageable.TakeDamage(999);
    }


}
