using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{

    public UnityEvent unityEvent;   
    bool used;

    [SerializeField] GameObject unclicked;
    [SerializeField] GameObject clicked;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag != "Player") return;
        if (used) return;

        unityEvent.Invoke();
        unclicked.SetActive(false);
        clicked.SetActive(true);
        //activate button.
        used = true;
    }
}
