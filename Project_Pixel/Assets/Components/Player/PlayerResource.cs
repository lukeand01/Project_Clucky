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
    public void ControlImmunity(bool choice) => IsImmune = choice;

    public void TakeDamage(float damage)
    {
        if (IsImmune) return;
        GameHandler.instance.LoseGame();

        IsImmune = true;
        StartCoroutine(DieProcess());
        
    }

    IEnumerator DieProcess()
    {
        yield return null;
    }



    
}
