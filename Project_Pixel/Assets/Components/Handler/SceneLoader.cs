using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    GameHandler handler;
    [SerializeField] Image blackScreen;
    public int currentScene = 0;

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

        //black screen increase
        blackScreen.gameObject.SetActive(true);
        while(blackScreen.color.a < 1)
        {
            blackScreen.color += new Color(0, 0, 0, 0.035f);
            yield return new WaitForSeconds(0.001f);
        }

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
        }

       
       if(PlayerHandler.instance != null)
        {
            PlayerHandler.instance.ResetPlayer();
            PlayerHandler.instance.FreezeRB(false);
            PlayerHandler.instance.UpdateMMUI();
        }



       if(ButtonInputHandler.instance != null)
        {           
           ButtonInputHandler.instance.ControlVisibilityOnInputs(scene != 0);        
        }

        
        //black screen decrease
        while (blackScreen.color.a > 0)
        {
            blackScreen.color -= new Color(0, 0, 0, 0.035f);
            yield return new WaitForSeconds(0.001f);
        }
        currentScene = scene;
        blackScreen.gameObject.SetActive(false);
        StopAllCoroutines();
        PlayerHandler.instance.block.ClearBlock();
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
