using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;

    public Observer observer;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        observer = new Observer();
    }


    public void CreateSFX(AudioClip clip)
    {

    }
}
