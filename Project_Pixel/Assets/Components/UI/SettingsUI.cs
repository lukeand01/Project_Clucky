using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    //here we change volume just one thing for now: sound.
    GameObject holder;
    [Separator("VOLUME ASSETS")]
    [SerializeField] SettingsVolumeUnit volumeUnitBGM;
    [SerializeField] SettingsVolumeUnit volumeUnitSFX;

    private void Awake()
    {
        holder = transform.GetChild(0).gameObject;
    }

    public void Open()
    {
        holder.SetActive(true);
    }
    public void Return()
    {
        holder.SetActive(false);
    }

}
