using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputButton : ButtonBase
{
    public float value;


    public UnityEvent unityEvent;

    #region EVENT
    public event Action EventPressed;
    public void OnPressed() => EventPressed?.Invoke();

    public event Action EventReleased;
    public void OnReleased() => EventReleased?.Invoke();
    #endregion


   

    private void OnDisable()
    {
        value = 0;      
    }




    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        value = 1;
        unityEvent.Invoke();
        OnPressed();

    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (value == 1) OnReleased();
        value = 0;


    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
    }


}
