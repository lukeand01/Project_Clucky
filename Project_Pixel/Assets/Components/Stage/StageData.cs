using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage / Stage")]
public class StageData : ScriptableObject
{
    public int stageID;
    public string stageName;
    public Sprite stageSprite;
    [TextArea]public string stageDescription;
    
}
