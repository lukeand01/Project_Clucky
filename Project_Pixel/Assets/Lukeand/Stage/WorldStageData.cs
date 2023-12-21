using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage / World")]
public class WorldStageData : ScriptableObject
{
    public int worldIndex;
    public string worldName;
    public List<StageData> stageList = new();

    public void SetIndex(int worldIndex)
    {
        this.worldIndex = worldIndex;

        for (int i = 0; i < stageList.Count; i++)
        {
            stageList[i].SetIndex(worldIndex, i);
        }
    }

    public void ResetStages()
    {
        foreach (var item in stageList)
        {
            item.ResetList();
        }
    }
}
