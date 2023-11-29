using MyBox;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LocalHandler : MonoBehaviour
{
    //this is supposed to care about information only relevant to this scene.

    public static LocalHandler instance;

    public int localGoldStored;
    public int localGoldGained;
    public int localGoldTotal;

    public StageData stageData;

    [SerializeField]List<int> momentarilyCoinAddList = new();

    SurvivalHandler survival;


    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(instance.gameObject);
            instance = this;
        }
    }

    private void Start()
    {
        //count the coin.
        //why?
        //


        //steps
        //when the game first loads we check

        survival = GetComponent<SurvivalHandler>();
        if (survival != null) survival.StartSurvival();

        RemoveCoinsBasedInSave();

        if(GameHandler.instance != null) GameHandler.instance.sound.ChangeBGM(stageData.backgroundClip);
    }

    void RemoveCoinsBasedInSave()
    {
        GameObject[] coinInScene = GameObject.FindGameObjectsWithTag("Coin");


        for (int i = 0; i < coinInScene.Length; i++)
        {
            coinInScene[i].GetComponent<Coin>().SetIndex(i);
        }

        foreach (var item in stageData.coinObtainedList)
        {
            if (item >= stageData.howManyCoinInScene)
            {
                Debug.LogError("something wrong");
            }
            else
            {
                coinInScene[item].SetActive(false);
            }


        }
    }

    void COIN1()
    {
        GameObject[] coinInScene = GameObject.FindGameObjectsWithTag("Coin");


        for (int i = 0; i < coinInScene.Length; i++)
        {
            coinInScene[i].GetComponent<Coin>().SetIndex(i);
        }

        localGoldTotal = coinInScene.Length;


        if (stageData == null)
        {
            Debug.Log("stage dadta is null");
        }
        if (stageData.coinObtainedList == null)
        {
            Debug.Log("th coin list is null");
        }
        localGoldStored = stageData.coinObtainedList.Count;

        foreach (var item in stageData.coinObtainedList)
        {
            if (item >= stageData.howManyCoinInScene)
            {
                Debug.LogError("something wrong");
            }
            else
            {
                coinInScene[item].SetActive(false);
            }


        }
    }


    [SerializeField] Transform initialPos;


    

    public void InitScene()
    {
        PlayerHandler handler = PlayerHandler.instance;
        Transform pos = handler.transform;

        handler.cam.ForceCameraIntoTransform(pos);


        pos.position = initialPos.position;

        momentarilyCoinAddList.Clear();
        

        localGoldStored = stageData.coinObtainedList.Count;
        localGoldGained = 0;

        UIHolder.instance.player.UpdateCoin(localGoldStored, localGoldGained, stageData.howManyCoinInScene);


        UIHolder.instance.death.StopDeathUI();
        UIHolder.instance.victory.StopVictoryUI();
        UIHolder.instance.pause.Force(false);
        if(originalEgg == null)
        {
            Egg newEgg = GameHandler.instance.eggTemplate;
            Instantiate(newEgg, originalEggPos.position, Quaternion.identity);
        }


        
    }

    public void WinScene()
    {
        //only call this when you win.
        //should we save here?
        //also we pass the new valuee of things to the player.
        PlayerHandler.instance.AddCoin(localGoldGained);


        foreach (var item in momentarilyCoinAddList)
        {
            stageData.coinObtainedList.Add(item);
        }
        
        //must save this file. but how do i save it again?
        
    }
    public void LoseScene()
    {
        
        //momentarilyCoinAddList.Clear();
    }

    void AddCoin(int index)
    {
        momentarilyCoinAddList.Add(index);
        localGoldGained = momentarilyCoinAddList.Count;
        
    }

    public void AddLocalGold(int index)
    {
        //there is the gold      
        AddCoin(index);
        UIHolder.instance.player.UpdateCoin(localGoldStored + localGoldGained, 1, stageData.howManyCoinInScene);
    }


    //



    [Separator("EGG")]
    [SerializeField] Egg originalEgg;
    [SerializeField] Transform originalEggPos;
}
