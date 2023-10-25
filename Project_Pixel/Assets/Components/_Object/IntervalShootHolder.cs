using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalShootHolder : MonoBehaviour
{
    //
    [SerializeField]IntervalShootScript[] intervalShooters;

    public void StartShooters()
    {
        foreach (var item in intervalShooters)
        {
            item.StartShooting();
        }
    }

    public void StopShooters()
    {
        foreach (var item in intervalShooters)
        {
            item.StopShooting();
        }
    }



}
