using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{

    AudioSource BGMSource;

    public float currentBGMVolume { get; private set; }
    public float currentSFXVolume { get; private set; }

    [SerializeField]GameObject sfcContainer;

    private void Awake()
    {
        if (BGMSource == null) BGMSource = gameObject.AddComponent<AudioSource>();

        BGMSource.loop = true;

        currentBGMVolume = 0.7f;
        currentSFXVolume = 0.8f;

        BGMSource.volume = currentBGMVolume;

    }


    public void ChangeSFXVolume(float volume)
    {
        currentSFXVolume = volume;
    }


    public void ChangeBGMVolume(float value)
    {
        currentBGMVolume += value;
        currentBGMVolume = Mathf.Clamp(currentBGMVolume, 0, 1);
        BGMSource.volume = currentBGMVolume;
    }
    public void SetBGMVolume(float value)
    {
        currentBGMVolume = value;
        BGMSource.volume = currentBGMVolume;
    }



    public void CreateSFX(AudioClip clip)
    {
        GameObject newObject = new GameObject();
        newObject.transform.parent = sfcContainer.transform;
        newObject.AddComponent<DestroySelf>().SetUp(clip.length + 0.1f);
        AudioSource audio = newObject.AddComponent<AudioSource>();
        audio.volume = currentBGMVolume;
        audio.clip = clip;
        audio.Play();
    }





    public void ChangeBGM(AudioClip clip, float rate = 0)
    {
        //its a process where you slolwy reduc the sound 
        StopAllCoroutines();

        if(rate == 0)
        {
            BGMSource.Stop();
            BGMSource.clip = clip;
            BGMSource.Play();
        }
        else
        {
            StartCoroutine(ChangeBGMProcess(clip, rate));

        }

        

    }

    IEnumerator ChangeBGMProcess(AudioClip clip, float rate)
    {

        while(BGMSource.volume > 0)
        {
            BGMSource.volume -= 1 * rate;
            yield return new WaitForSeconds(0.1f);
        }

        BGMSource.clip = clip;

        while(BGMSource.volume < currentBGMVolume)
        {
            BGMSource.volume += 1 * rate;
            yield return new WaitForSeconds(0.1f);
        }
    }


}

