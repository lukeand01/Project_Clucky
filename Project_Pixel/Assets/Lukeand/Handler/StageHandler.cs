using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageHandler : MonoBehaviour
{
    public int stageCurrentProgress = 1;
    public List<WorldStageData> worldList = new();
    public int limitPhase;

    StageData currentStage;


    //how to know what exactly stage is the player on 
    //
    private void Awake()
    {
        GenerateLimitPhase();
        SetIndex();
    }

    public void SetCurrentStage(StageData currentStage)
    {
        this.currentStage = currentStage;
    }

    //what is the problem
    //the problm is that i want to sav what scene i am on. but for that i need to know the world and stage index.

    public void NextCurrentStage()
    {

        

        WorldStageData currentWorld = worldList[currentStage.worldIndex];

        int newStageIndex = currentStage.stageIndex + 1;

        if (newStageIndex == 20)
        {
            //load the menu back
            return;
        }

        if (currentStage.stageIndex >= currentWorld.stageList.Count)
        {
            //then wee go to to the next world
            
            if(currentStage.worldIndex + 1 > worldList.Count)
            {
                Debug.Log("there are no more words");
            }
            else
            {
                currentWorld = worldList[currentStage.worldIndex + 1];
                newStageIndex = 0;
            }
        }

        

        currentStage = currentWorld.stageList[newStageIndex];


        if(currentStage.stageID > stageCurrentProgress)
        {
            stageCurrentProgress++;
        }

    }


    public int GetCurretScene()
    {
        if (currentStage == null) return -1;

        return currentStage.stageID;
    }

    public void ReceiveSaveData(SaveClass save)
    {
        stageCurrentProgress = save.playerProgress;

        //stagehandler.

        
        foreach (var item in save.stageSaveList)
        {
            WorldStageData world = worldList[item.worldID];
            StageData stage = world.stageList[item.stageID];
            stage.ReceiveSave(item.obtainedGoldList);
        }
    }


    public void ResetStage()
    {
        foreach (var item in worldList)
        {
            item.ResetStages();
        }
    }


    public StageData GetCurrentStage() => currentStage;

    void SetIndex()
    {
        for (int i = 0; i < worldList.Count; i++)
        {
            worldList[i].SetIndex(i);
        }
    }
    void GenerateLimitPhase()
    {
        //this is created so we dont call for \
        
        foreach (var world in worldList)
        {
            foreach (var stage in world.stageList)
            {
                limitPhase++;
            }
        }

    }

    public bool IncreaseStageProgress(int currentScene)
    {
        //if its the current one fella.
        //but only if i am in the last


        if(currentScene >= 19)
        {
            Debug.Log("this?");
            return false;
        }

        if(currentScene >= limitPhase)
        {
            Debug.Log("there are no more phases");
            return false;
        }

        if(currentScene == stageCurrentProgress)
        {
            stageCurrentProgress++;
        }

        return true;
    }




}
