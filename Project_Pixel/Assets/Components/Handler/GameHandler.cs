using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;

    [HideInInspector] public Observer observer;
    [HideInInspector] public StageHandler stage;
    [HideInInspector] public SceneLoader loader;


    [Separator("TEMPLATES")]
    public Egg eggTemplate;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        observer = new Observer();
        stage = gameObject.GetComponent<StageHandler>();
        loader = gameObject.GetComponent<SceneLoader>();
    }


    public void CreateSFX(AudioClip clip)
    {

    }


    public void ProgressToNextStage()
    {
        //


        if (stage.IncreaseStageProgress(loader.currentScene))
        {
            loader.LoadNextStage();
        }
        else
        {
            loader.ChangeScene(0);
        }

        
    }

    public void WinGame()
    {
        PlayerHandler.instance.block.AddBlock("Win", BlockClass.BlockType.Complete);
        PlayerHandler.instance.resource.ControlImmunity(true);
        UIHolder.instance.victory.StartVictoryUI();
    }
    public void LoseGame()
    {
        PlayerHandler.instance.block.AddBlock("Lose", BlockClass.BlockType.Complete);
        UIHolder.instance.death.StartDeathUI();
    }




}
