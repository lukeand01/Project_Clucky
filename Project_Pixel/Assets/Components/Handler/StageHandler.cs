using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageHandler : MonoBehaviour
{
    public int stageCurrentProgress = 1;
    public List<WorldStageData> worldList = new();
    public int limitPhase;

    private void Awake()
    {
        GenerateLimitPhase();
    }

    void GenerateLimitPhase()
    {
        //this is created so we dont call for \
        
        foreach (var world in worldList)
        {
            foreach (var stage in world.stageList)
            {
                limitPhase++;
            }
        }

    }

    public bool IncreaseStageProgress(int currentScene)
    {
        //if its the current one fella.
        //but only if i am in the last

        if(currentScene >= limitPhase)
        {
            Debug.Log("there are no more phases");
            return false;
        }

        if(currentScene == stageCurrentProgress)
        {
            stageCurrentProgress++;
        }

        return true;
    }

}
