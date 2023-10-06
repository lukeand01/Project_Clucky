using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage / Stage")]
public class StageData : ScriptableObject
{
    [HideInInspector] public int worldIndex; //this is just for help finding.
    [HideInInspector] public int stageIndex;

    public int stageID;

    [ContextMenu("RESET COIN LIST")]
    public void ResetList()
    {
        coinObtainedList.Clear();
    }

    public void SetIndex(int worldIndex, int stageIndex)
    {
        this.worldIndex = worldIndex;
        this.stageIndex = stageIndex;
    }


    public string stageName;
    public Sprite stageSprite;
    [TextArea]public string stageDescription;

    //i can maybe just make a int list of all that were obtained.
    public int howManyCoinInScene;
    public List<int> coinObtainedList = new();


    public void ReceiveSave(List<int> coinObtainedList)
    {


        this.coinObtainedList.Clear();

        foreach (var item in coinObtainedList)
        {
            this.coinObtainedList.Add(item);
        }

       
    }

    //this is the pos of the thing.
    //and so anytime the locahandler starts we check all coins based on that list.

}

