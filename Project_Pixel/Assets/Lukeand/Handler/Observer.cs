using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer
{
    public event Action<bool> EventChangePlataform;
    public void OnChangePlataform(bool choice) => EventChangePlataform?.Invoke(choice);

    public event Action<int> EventMMUpdateGold;
    public void OnMMUpdateGold(int gold) => EventMMUpdateGold?.Invoke(gold);
}
