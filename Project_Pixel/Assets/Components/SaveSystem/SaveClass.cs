using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SaveClass 
{
    //data to save
    //player progress (int)
    //player gold (int)
    //gold obtained list (list<int>)
    //

    public SaveClass()
    {
        playerProgress = 1;
        playerGold = 0;
    }


    public int playerProgress;
    public int playerGold;

    public List<StageSaveClass> stageSaveList = new();

    //when i start the game i want to save all the saves

    int GetStageIndex(int worldID, int stageID)
    {
        for (int i = 0; i < stageSaveList.Count; i++)
        {
            if (stageSaveList[i].worldID != worldID) continue;
            if (stageSaveList[i].stageID != stageID) continue;
            return i;
        }
        
        return -1;
    }

    public void SaveNewStage(int worldID, int stageID, List<int> obtainedGoldList)
    {
        //we check if we already have it.
        int index = GetStageIndex(worldID, stageID);


        if (index == -1)
        {
            //create a new one.
            StageSaveClass newStage = new StageSaveClass(worldID, stageID, obtainedGoldList);
            stageSaveList.Add(newStage);
        }
        else
        {
            stageSaveList[index].SetUpList(obtainedGoldList);
        }

    }

}

[System.Serializable]
public class StageSaveClass
{
    public StageSaveClass(int worldID, int stageID, List<int> obtainedGoldList)
    {
        this.worldID = worldID;
        this.stageID = stageID;
        this.obtainedGoldList = obtainedGoldList;
    }

    public void SetUpList(List<int> obtainedGoldList)
    {
        this.obtainedGoldList = obtainedGoldList;
    }

    public int worldID;
    public int stageID;

    public List<int> obtainedGoldList = new();
}