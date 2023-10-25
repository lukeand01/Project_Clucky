using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour, IPickable
{
    [SerializeField] AudioClip sfx;

    public string GetID()
    {
        return "";
    }

    public void Pick()
    {
        GameHandler.instance.WinGame();
        GameHandler.instance.sound.CreateSFX(sfx);
        Destroy(gameObject);
    }
}
