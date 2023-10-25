using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;

    [HideInInspector] public Observer observer;
    [HideInInspector] public StageHandler stageHandler;
    [HideInInspector] public SceneLoader loader;
    [HideInInspector] public SoundHandler sound;

    //i can keep the save data here?
    SaveClass save;

    //whn we load the game we check if there is some kind of save.
    //we send information to the player for now.

    string savePath;

    [Separator("TEMPLATES")]
    public Egg eggTemplate;

    [ContextMenu("DELETE SAVE")]
    public void DeleteSave()
    {
        SaveHandler2.DeleteData(savePath);
    }


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        savePath = "/TESTE";

        DontDestroyOnLoad(gameObject);

        observer = new Observer();
        stageHandler = gameObject.GetComponent<StageHandler>();
        loader = gameObject.GetComponent<SceneLoader>();
        sound = gameObject.GetComponent<SoundHandler>();
    }

    private void Start()
    {
        if (DEBUGCHECKSAVE) CheckSave();
        HandleLoad();
    }

    [SerializeField] bool DEBUGCHECKSAVE;
    public bool DEBUGANYSTAGE;
    void CheckSave()
    {
        if (SaveHandler2.HasFile(savePath))
        {
            save = SaveHandler2.LoadData<SaveClass>(savePath, false);

        }
        else
        {
            UnityEngine.Debug.Log("FOUND NO SAVE");
            stageHandler.ResetStage();
        }
    }


    //this 
    void HandleLoad()
    {
        SaveClass save = new SaveClass();

        if (SaveHandler2.HasFile(savePath))
        {
            save = SaveHandler2.LoadData<SaveClass>(savePath, false);
            if (save == null)
            {
                save = new SaveClass();
            }
        }
        else
        {
            save = new SaveClass();
        }

        //then we send info to theem so they can do their stuff.
        //but why is the gold displaying wrongly?
        //what am i saving?

        PlayerHandler.instance.ReceiveSaveData(save);
        stageHandler.ReceiveSaveData(save);

        
    }

    

    


    public void ProgressToNextStage()
    {
        //

        if (stageHandler.IncreaseStageProgress(loader.currentScene))
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
        //only when you win you update individual coins.


        LocalHandler.instance.WinScene();
      
        DealWithSave();
        stageHandler.NextCurrentStage();

        PlayerHandler.instance.block.AddBlock("Win", BlockClass.BlockType.Complete);
        PlayerHandler.instance.resource.ControlImmunity(true);
        UIHolder.instance.victory.StartVictoryUI();
    }

    void DealWithSave()
    {

        if (save == null)
        {
            save = new SaveClass();
        }

        save.playerGold = PlayerHandler.instance.PlayerGold;
        save.playerProgress = stageHandler.stageCurrentProgress;     

        //save this curreent fella
        StageData data = stageHandler.GetCurrentStage();

        if (data == null)
        {
            UnityEngine.Debug.Log("the current stage is null");
        }

        save.SaveNewStage(data.worldIndex, data.stageIndex, data.coinObtainedList);

        //then we add the fella we are currently in.
        //so i want to know whats the current level i am. 



        bool result = SaveHandler2.SaveData(savePath, save, false);

        if (!result)
        {
            DebugWarning("failed to save");
        }

        UnityEngine.Debug.Log("save result " + result);
    }



    public void LoseGame()
    {
        PlayerHandler.instance.block.AddBlock("Lose", BlockClass.BlockType.Complete);
        LocalHandler.instance.LoseScene();

        UIHolder.instance.death.StartDeathUI();
        UIHolder.instance.tutorial.StopTutorial();
    }



    #region DEBUGG
    [Separator("DEBUG")]
    [SerializeField] GameObject debugHolder;
    [SerializeField] TextMeshProUGUI debugText;

    public void DebugWarning(string debugWarning)
    {
        StartCoroutine(DebugProcess(debugWarning));
    }

    IEnumerator DebugProcess(string debugWarning)
    {
        debugText.text = "Warning! problem occured: " + debugWarning;

        debugHolder.SetActive(true);

        yield return new WaitForSeconds(5);

        debugHolder.SetActive(false);
    }


    #endregion


}
