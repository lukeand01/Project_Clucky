using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsHandler : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsShowListener
{
    //we handle all ads here.
    //every stage there will be a new ad.


    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;
    public bool isProcess { get; private set; }
    public bool debugDontSeeAds;

    private void Awake()
    {
       if(!debugDontSeeAds) InitAds();
    }

    void InitAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);

        


    }

    //
    public void PlayInterstialAD()
    {
        //play a interstial ad.
        bool isIphone = Application.platform == RuntimePlatform.IPhonePlayer;
        string nameID = "";
        isProcess = true;
        
        if (isIphone)
        {
            nameID = "Interstitial_iOS";
        }
        else
        {
            nameID = "Interstitial_Android";
        }

        if (!Advertisement.isShowing)
        {
            Advertisement.Show(nameID, this);
        }
        else
        {
            //and ad is already showing.
        }
       


    }

    public void PlayRewardAD()
    {
        bool isIphone = Application.platform == RuntimePlatform.IPhonePlayer;
        string nameID = "";
        Debug.Log("isiphone " + isIphone);
        isProcess = true;

        if (isIphone)
        {
            nameID = "Rewarded_iOS";
        }
        else
        {
            nameID = "Rewarded_Android";
        }

        if (!Advertisement.isShowing)
        {
            Advertisement.Show(nameID, this);
        }
        else
        {
            //and ad is already showing.
        }
    }

    public void StartBannerAD()
    {
        //
    }

    public void OnInitializationComplete()
    {
        //Debug.Log("unity ads initialization complete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Unit ads initialization failed: " + error.ToString());
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        
    }

    public void OnUnityAdsShowClick(string placementId)
    {

    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {

        Debug.Log("completed an ad");

        if(placementId == "Rewarded_Android" && showCompletionState == UnityAdsShowCompletionState.COMPLETED 
            || placementId == "Rewarded_iOS" && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("should reward the player");
            //give a mineral to the player.
            isProcess = false;
            PlayerHandler.instance.AddCoin(5);


        }

        if (placementId == "Interstitial_Android" && showCompletionState == UnityAdsShowCompletionState.COMPLETED
            || placementId == "Interstitial_iOS" && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            isProcess = false;
        }


    }
}
