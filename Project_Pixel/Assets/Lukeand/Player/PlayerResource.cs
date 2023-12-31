using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResource : MonoBehaviour, IDamageable
{

    PlayerHandler handler;
    bool IsImmune;

    private void Awake()
    {
        handler = GetComponent<PlayerHandler>();
    }

    public void ResetResource()
    {
        IsImmune = false;
    }
    public void ControlImmunity(bool choice)
    {
        IsImmune = choice;
        gameObject.layer = choice ? 8 : 3;
    }

    public void TakeDamage(float damage)
    {

        if (handler.DEBUGisImmune) return;
        if (IsImmune) return;
        GameHandler.instance.LoseGame();

        IsImmune = true;
        GameHandler.instance.sound.CreateSFX(handler.deathSFX, transform);
        StartCoroutine(DieProcess());
        
    }

    IEnumerator DieProcess()
    {
        yield return null;
    }



    
}
