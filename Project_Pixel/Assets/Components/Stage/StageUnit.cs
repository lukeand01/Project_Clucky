using MyBox;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageUnit : ButtonBase
{
    [Separator("STAGE")]
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] GameObject blocked;
    [SerializeField] GameObject selected;

    StageUI handler;
    StageData data;
    
    public void SetUp(StageData data, StageUI handler, int currentStage)
    {
        this.data = data;
        this.handler = handler;

        nameText.text = data.stageName;
        coinText.text = "Coin: " + data.coinObtainedList.Count.ToString() + " / " + data.howManyCoinInScene.ToString();
        blocked.SetActive(currentStage < data.stageID);

        if (GameHandler.instance.DEBUGANYSTAGE)
        {
            blocked.SetActive(false);
        }
    }


    //what defines if a levl is blocked or not its
    
    public void Select(bool choice)
    {
        selected.SetActive(choice);
    }


    public override void OnPointerClick(PointerEventData eventData)
    {
        //ordeer the thing
        if (blocked.activeInHierarchy) return;

      
        handler.SelectStage(data);
        handler.SelectStageUnit(this);
    }

}
