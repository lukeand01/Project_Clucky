using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsVolumeUnit : MonoBehaviour
{
    //this thing controls all the aspect of the volume.

    [SerializeField] VolumeType type;
    [SerializeField] Image mainImage;

    [SerializeField] Sprite volumeSprite;
    [SerializeField] Sprite noVolumeSprite;

    [SerializeField] GameObject[] volumeIndicators;

    float volumeSynch;
    bool isMute;

    private void Start()
    {
        //ask for th gamehandler what is the currentvolume;

        if(type == VolumeType.BackgroundMusic)
        {
            volumeSynch = GameHandler.instance.sound.currentBGMVolume;
        }
        if(type == VolumeType.SFX)
        {
            volumeSynch = GameHandler.instance.sound.currentSFXVolume;
        }

        UpdateVolumeIndicator(volumeSynch * 10f);
    }

    #region BUTTONS FUNCTIONS
    public void IncreaseVolume()
    {

        if (type == VolumeType.BackgroundMusic)
        {
            GameHandler.instance.sound.ChangeBGMVolume(0.1f);
        }
        if (type == VolumeType.SFX)
        {
            
        }

        volumeSynch += 0.1f;
        volumeSynch = Mathf.Clamp(volumeSynch, 0, 1);
        UpdateVolumeIndicator(volumeSynch * 10f);
        UpdateIcon();
    }
    public void DecreaseVolume()
    {
        if (type == VolumeType.BackgroundMusic)
        {
            GameHandler.instance.sound.ChangeBGMVolume(-0.1f);
        }
        if (type == VolumeType.SFX)
        {

        }


        volumeSynch -= 0.1f;
        volumeSynch = Mathf.Clamp(volumeSynch, 0, 1);
        UpdateVolumeIndicator(volumeSynch * 10);
        UpdateIcon();
    }

    public void MuteVolume()
    {
        if (type == VolumeType.BackgroundMusic)
        {
            if (isMute)
            {
                GameHandler.instance.sound.SetBGMVolume(volumeSynch);
            }
            else
            {
                GameHandler.instance.sound.SetBGMVolume(0);
            }           
        }
        if (type == VolumeType.SFX)
        {

        }
        Debug.Log("this is the thing " + isMute + " opposite " + !isMute);
        UpdateIcon(!isMute);
        isMute = !isMute;
    }
    #endregion

    void UpdateIcon(bool isForce = false)
    {
        if (isForce)
        {
            mainImage.sprite = noVolumeSprite;
            return;
        }

        if (volumeSynch > 0)
        {
            mainImage.sprite = volumeSprite;
        }
        else
        {
            mainImage.sprite = noVolumeSprite;
        }

    }
    void UpdateVolumeIndicator(float currentVolume)
    {
        for (int i = 0; i < volumeIndicators.Length; i++)
        {
            volumeIndicators[i].SetActive(i <= currentVolume);         
        }
    }


}

public enum VolumeType
{
    BackgroundMusic,
    SFX
}