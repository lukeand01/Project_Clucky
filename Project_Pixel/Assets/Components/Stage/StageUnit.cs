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
    [SerializeField] TextMeshProUGUI text;

    StageUI handler;
    StageData data;
    
    public void SetUp(StageData data, StageUI handler)
    {
        this.data = data;
        this.handler = handler;
    }


    public override void OnPointerClick(PointerEventData eventData)
    {
       //ordeer the thing
        
    }

}
