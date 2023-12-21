using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvent : MonoBehaviour
{
    public UnityEvent events;

    bool hasInvoked;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        if(!hasInvoked)
        {
            events.Invoke();
            hasInvoked = true;
        }
            
    }

}
