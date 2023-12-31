using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    GameHandler handler;
    [SerializeField] Image blackScreen;
    [SerializeField] TextMeshProUGUI loadingText;

    public int currentScene = 0;
    bool nextStageLoadsAD;


    private void Awake()
    {
        handler = GetComponent<GameHandler>();
    }

   


    public void ChangeScene(int scene, StageData stage = null)
    {
        StopAllCoroutines();
        handler.stageHandler.SetCurrentStage(stage);
        StartCoroutine(ChangeSceneProcess(scene, false));
        StartCoroutine(LoadingProcess());
    }

    public void ResetScene()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeSceneProcess(currentScene, true));
    }



    IEnumerator ChangeSceneProcess(int scene, bool isReset)
    {
        //stop everything.
        if(PlayerHandler.instance != null)
        {
            //also freeze char.
            PlayerHandler.instance.FreezeRB(true);
            PlayerHandler.instance.block.AddBlock("Loader", BlockClass.BlockType.Complete);
        }

        //THIS IS JUST FOR TESTING

        //also everytime is a new phase.
        GameHandler handler = GameHandler.instance;

        if(!handler.ad.debugDontSeeAds)
        {
            if (scene >= handler.stageHandler.stageCurrentProgress)
            {
                Debug.Log("checked dthis instead");
                AdsHandler adHandler = GameHandler.instance.ad;
                adHandler.PlayInterstialAD();

                while (adHandler.isProcess)
                {
                    Debug.Log("Process");
                    yield return null;
                }
                Debug.Log("ad is done");
            }
            else
            {
                if (nextStageLoadsAD)
                {
                    if (scene != 0)
                    {
                        //then we play an ad before anything else.
                        Debug.Log("should have loaded a scene");
                        AdsHandler adHandler = GameHandler.instance.ad;
                        adHandler.PlayInterstialAD();

                        while (adHandler.isProcess)
                        {
                            Debug.Log("Process");
                            yield return null;
                        }
                        Debug.Log("add is done");
                    }


                }
                else
                {
                    nextStageLoadsAD = true;
                }
            }

        }



        //black screen increase
        blackScreen.gameObject.SetActive(true);
        while(blackScreen.color.a < 1)
        {
            blackScreen.color += new Color(0, 0, 0, 0.035f);
            yield return new WaitForSeconds(0.001f);
        }

        StartCoroutine(LoadingAnimationProcess());

        yield return StartCoroutine(LowerCurtainProcess());


        //unload the scene.

        if (!isReset)
        {
            UnityEngine.AsyncOperation loadAsync = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

            while (!loadAsync.isDone)
            {
                yield return new WaitForSeconds(0.01f);
            }

            UnityEngine.AsyncOperation unloadAsync = SceneManager.UnloadSceneAsync(currentScene, UnloadSceneOptions.None);

            while (!unloadAsync.isDone)
            {
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            SceneManager.LoadScene(scene);
        }

           
       yield return new WaitForSeconds(0.05f);


       if(scene != 0)
        {
            while(LocalHandler.instance == null)
            {
                yield return new WaitForSeconds(0.01f);
            }
            LocalHandler.instance.InitScene();


            bool isCameraInitialized = PlayerHandler.instance.cam.IsCameraCentralized();

            

        }
        

       
       if(PlayerHandler.instance != null)
        {
            PlayerHandler.instance.ResetPlayer();
            PlayerHandler.instance.FreezeRB(false);
            PlayerHandler.instance.UpdateMMUI();
            PlayerHandler.instance.cam.ControlCameraUse(scene == 0);
        }



       if(ButtonInputHandler.instance != null)
        {           
           ButtonInputHandler.instance.ControlVisibilityOnInputs(scene != 0);        
        }

        yield return new WaitForSeconds(1);

        //black screen decrease

        if(BackgroundAnimatorScript.instance != null)
        {
            BackgroundAnimatorScript.instance.ForceSpawnIfEmpty();
        }


        while (blackScreen.color.a > 0)
        {

            blackScreen.color -= new Color(0, 0, 0, 0.035f);
            yield return new WaitForSeconds(0.001f);
        }

        yield return StartCoroutine(RaiseCurtainProcess());
        StopAllCoroutines();

        currentScene = scene;
        blackScreen.gameObject.SetActive(false);
        StopAllCoroutines();
        PlayerHandler.instance.block.ClearBlock();
    }

    IEnumerator LoadingAnimationProcess()
    {
        loadingText.text = "Loading";
        yield return new WaitForSeconds(0.2f);
        loadingText.text = "Loading.";
        yield return new WaitForSeconds(0.2f);
        loadingText.text = "Loading..";
        yield return new WaitForSeconds(0.2f);
        loadingText.text = "Loading...";
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(LoadingAnimationProcess());
    }


    IEnumerator RaiseCurtainProcess()
    {
        //we need information about how big the screen is and we need to set up how big?



        yield return null;
    }
    IEnumerator LowerCurtainProcess()
    {
        yield return null;
    }


    IEnumerator LoadingProcess()
    {
        yield return null;
    }


    public void LoadNextStage()
    {
        //need to check the limit.
              
        int nextScene = handler.stageHandler.GetCurretScene();
        
        if(nextScene == -1)
        {
            Debug.LogError("something wrong");
            return;
        }


       StartCoroutine(ChangeSceneProcess(nextScene, false));
    }


}
