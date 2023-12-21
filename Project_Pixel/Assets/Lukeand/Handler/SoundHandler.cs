using MyBox;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{

    AudioSource BGMSource;
    [SerializeField] bool PLEASENOBGM;
    public float currentBGMVolume { get; private set; }
    public float currentSFXVolume { get; private set; }

    [Separator("SFX")]
    [SerializeField] AudioSource sfxTemplate;


    private void Awake()
    {
        if (BGMSource == null) BGMSource = gameObject.AddComponent<AudioSource>();

        BGMSource.loop = true;

        currentBGMVolume = 0.15f;
        currentSFXVolume = 0.8f;

        if (PLEASENOBGM)
        {
            currentBGMVolume = 0;

        }

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



    public void CreateSFX(AudioClip clip, Transform parent, float maxDistance  = 8)
    {
        AudioSource newObject = Instantiate(sfxTemplate, parent.position, Quaternion.identity);
        newObject.transform.parent = parent.transform;
        newObject.AddComponent<DestroySelf>().SetUp(clip.length + 0.1f);


        
        newObject.maxDistance = maxDistance;

        newObject.volume = currentSFXVolume;
        newObject.clip = clip;
        newObject.Play();
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

