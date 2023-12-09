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
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] GameObject blocked;


    StageUI handler;
    StageData data;

    GameObject holder;
    [SerializeField] GameObject depthImage;
    [SerializeField] GameObject selected; 
    Vector3 originalPos;
    Vector3 selectedPos;


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


        holder = transform.GetChild(1).gameObject;

        originalPos = holder.transform.localPosition;
        selectedPos = originalPos + new Vector3(0, -3, 0);

    }


    //what defines if a levl is blocked or not its
    
    public void Select(bool choice)
    {
        depthImage.SetActive(!choice);
        selected.SetActive(choice);
        if(choice)
        {
            holder.transform.localPosition = selectedPos;
        }
        else
        {
            holder.transform.localPosition = originalPos;
        }


    }


    public override void OnPointerClick(PointerEventData eventData)
    {
        //ordeer the thing
        if (blocked.activeInHierarchy) return;     
        handler.SelectStage(data);
        handler.SelectStageUnit(this);
    }

}

//how to improve this
//efect when selecting
//sound as well
//