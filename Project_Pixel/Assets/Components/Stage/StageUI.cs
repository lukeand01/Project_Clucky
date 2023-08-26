using MyBox;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] StageUnit unitTemplate;
    [SerializeField] GameObject containerTemplate;
    [SerializeField] Transform containerHolder;

    [Separator("SCREEN INFO")]
    [SerializeField] TextMeshProUGUI worldName;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject playButton;


    [Separator("DESCRIPTION")]
    [SerializeField] GameObject descriptionHolder;
    [SerializeField] TextMeshProUGUI descriptionNameText;
    [SerializeField] Image descriptionPortrait;
    [SerializeField] TextMeshProUGUI descriptionCoinText;
    [SerializeField] TextMeshProUGUI descriptionInfoText;
    

    //insteaad of creating this things all the time is better to create eveeryone at once.

    Dictionary<WorldStageData, GameObject> containerDictionary = new();
    List<GameObject> containerList = new();
    List<WorldStageData> worldDataList = new();

    int currentIndex;


    private void Start()
    {
        List<WorldStageData> worldList = GameHandler.instance.stage.worldList;
        CreateContainers(worldList);
        UpdateWorldUI();
    }
    #region CONTROL CONTAINERS
    public void CreateContainers(List<WorldStageData> worldList)
    {

        int currentStage = GameHandler.instance.stage.stageCurrentProgress;

        foreach (WorldStageData world in worldList)
        {
            GameObject newContainer = Instantiate(containerTemplate, containerTemplate.transform.position, Quaternion.identity);
            newContainer.transform.parent = containerHolder;
            newContainer.SetActive(false);
            newContainer.transform.localScale = new Vector3(1, 1, 1);
            containerDictionary.Add(world, newContainer);
            containerList.Add(newContainer);
            worldDataList.Add(world);

            foreach (StageData stage in world.stageList)
            {
                StageUnit newObject = Instantiate(unitTemplate, Vector3.zero, Quaternion.identity);
                newObject.SetUp(stage, this, currentStage);
                newObject.transform.parent = newContainer.transform;
            }
        }

        //leave just the first one open.
        currentIndex = 0;
        SetButtons();
        SelectStage(null);
        SelectStageUnit(null);
        ControlContainerIndex(currentIndex);
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

    #endregion

    void UpdateWorldUI()
    {
        worldName.text = worldDataList[0].worldName;
    }




    #region SELECTING 
    StageData stageData;
    StageUnit currentUnit;

    public void SelectStage(StageData stageData)
    {
        this.stageData = stageData;

        if(stageData == null)
        {
            descriptionHolder.SetActive(false);
            playButton.gameObject.SetActive(false);
            return;
        }
        playButton.gameObject.SetActive(true);
        descriptionHolder.SetActive(true);
        descriptionNameText.text = stageData.stageName;
        descriptionInfoText.text = stageData.stageDescription;

        if(stageData.stageSprite != null)
        {
            descriptionPortrait.gameObject.SetActive(false);
            descriptionPortrait.sprite = stageData.stageSprite;
        }
        else
        {
            descriptionPortrait.gameObject.SetActive(false);
        }

    }

    public void SelectStageUnit(StageUnit stageUnit)
    {      
        if(currentUnit != null)
        {
            currentUnit.Select(false);
        }

        if (stageUnit == null) return;

        stageUnit.Select(true);
        currentUnit = stageUnit;
    }

    public void Play()
    {
        //tell the game handler to load the stagedata id scene.

        if (stageData == null) return;
        //load the scene
        //the scene itslf always starts in the same place.

        if (GameHandler.instance == null) return;

        GameHandler.instance.loader.ChangeScene(stageData.stageID);
    }

    void SetButtons()
    {
        backButton.SetActive(false);
        if(containerList.Count > 1)
        {
            nextButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(false);
        }
    }

    public void NextWorld()
    {
        currentIndex++;
        if(currentIndex + 1 > containerList.Count)
        {
            nextButton.SetActive(false);
        }
        backButton.SetActive(false);
    }

    public void BackWorld()
    {
        currentIndex--;

        if(currentIndex == 0)
        {
            backButton.SetActive(false);
        }

        nextButton.SetActive(true);

    }


    #endregion


}
