using MyBox;
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

    [Separator("SOUND")]
    [SerializeField] AudioClip useSFX;

    public void ReturnLeverToOriginalState()
    {
        unclicked.SetActive(true);
        clicked.SetActive(false);
        used = false;
    }

    public void UseLever()
    {
        unclicked.SetActive(false);
        clicked.SetActive(true);
        used = true;
        GameHandler.instance.sound.CreateSFX(useSFX, transform);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        if (used) return;

        unityEvent.Invoke();
        UseLever();
    }
}
