using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    [SerializeField] StageUnit unitTemplate;
    [SerializeField] GameObject containerTemplate;
    [SerializeField] Transform containerHolder;

    //insteaad of creating this things all the time is better to create eveeryone at once.

    Dictionary<WorldStageData, GameObject> containerDictionary = new();
    List<GameObject> containerList = new();

    int currentIndex;

    public void CreateContainers(List<WorldStageData> worldList)
    {
        foreach (WorldStageData world in worldList)
        {
            GameObject newContainer = Instantiate(containerTemplate, containerTemplate.transform.position, Quaternion.identity);
            newContainer.transform.parent = containerHolder;
            newContainer.SetActive(false);
            containerDictionary.Add(world, newContainer);
            containerList.Add(newContainer);

            foreach (var stage in world.stageList)
            {
                StageUnit newObject = Instantiate(unitTemplate, Vector3.zero, Quaternion.identity);
                newObject.SetUp(stage, this);
                newObject.transform.parent = newContainer.transform;
            }
        }

        //leave just the first one open.
        ControlContainerIndex(0);
    }
  
    public void ResetAllContainers()
    {
        foreach (var item in containerList)
        {
            item.SetActive(false);
        }
    }

    public void ControlContainer(WorldStageData world, bool choice = true)
    {
        if(!containerDictionary.ContainsKey(world))
        {
            Debug.Log("couldnt find this fella " + world.worldName);
            return;
        }

        containerDictionary[world].SetActive(choice);
    }

    public void ControlContainerIndex(int index, bool choice = true)
    {
        currentIndex = index;
        if(index > containerList.Count)
        {
            currentIndex = 0;
        }
        if(index < 0)
        {
            currentIndex = containerList.Count;
        }

        containerList[currentIndex].SetActive(choice);

        
    }
}
