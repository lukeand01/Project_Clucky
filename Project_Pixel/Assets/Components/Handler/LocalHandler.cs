using MyBox;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LocalHandler : MonoBehaviour
{
    //this is supposed to care about information only relevant to this scene.

    public static LocalHandler instance;

    public int localGoldObtained;
    public int localGoldTotal;


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
        GameObject[] coinInScene = GameObject.FindGameObjectsWithTag("Coin");


        foreach (var item in coinInScene)
        {
            localGoldTotal++;
        }

    }


    [SerializeField] Transform initialPos;

    public void InitScene()
    {
        Transform pos = PlayerHandler.instance.transform;

        pos.position = initialPos.position;

        localGoldObtained = 0;
        UIHolder.instance.player.UpdateCoin(localGoldObtained);


        UIHolder.instance.death.StopDeathUI();
        UIHolder.instance.victory.StopVictoryUI();
        UIHolder.instance.pause.Force(false);
        if(originalEgg == null)
        {
            Debug.Log("somthing wrong about the egg");
            Egg newEgg = GameHandler.instance.eggTemplate;
            Instantiate(newEgg, originalEggPos.position, Quaternion.identity);
        }
    }

    public void AddLocalGold()
    {
        localGoldObtained += 1;
        localGoldObtained = Mathf.Clamp(localGoldObtained, 0, localGoldTotal);

        UIHolder.instance.player.UpdateCoin(localGoldObtained);
    }


    [Separator("EGG")]
    [SerializeField] Egg originalEgg;
    [SerializeField] Transform originalEggPos;
}
