using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage / World")]
public class WorldStageData : ScriptableObject
{
    public string worldName;
    public List<StageData> stageList = new();

}
