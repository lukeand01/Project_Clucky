using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultipleReceiver : MonoBehaviour
{
    [SerializeField] UnityEvent unityEvent;
    [SerializeField] int total;
    [SerializeField] AudioClip doneAudioClip;
    int current;

    public void SingleReceive()
    {
        Debug.Log("receive");
        current += 1;
        if (current >= total) DoEvent();
    }

    void DoEvent()
    {
        Debug.Log("event done");
        unityEvent.Invoke();
        if (doneAudioClip != null) GameHandler.instance.sound.CreateSFX(doneAudioClip, PlayerHandler.instance.transform);
    }

}
