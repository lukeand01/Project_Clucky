using MyBox;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    //ij here we buy stuff
    GameObject holder;
    [SerializeField] CharacterUnit template;
    [SerializeField] Transform container;

    private void Awake()
    {
        holder = transform.GetChild(0).gameObject;
    }

    public void Open()
    {
        holder.SetActive(true);
    }
    public void Close()
    {
        holder.SetActive(false);
    }

    public void StartUI(List<CharacterData> dataList)
    {

        ClearTargetContainer(container);
        foreach (var item in dataList)
        {
            CharacterUnit newObject = Instantiate(template, Vector2.zero, Quaternion.identity);
            newObject.transform.parent = container;
            newObject.SetUp(item, this);
        }
    }



    [Separator("SELECT")]
    [SerializeField] GameObject selectorHolder;
    [SerializeField] TextMeshProUGUI characterNameText;
    [SerializeField] TextMeshProUGUI characterDescriptionText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] GameObject coinImage;
    [SerializeField] GameObject boughtText;
    [SerializeField] ButtonBase buyButton;

    CharacterUnit currentUnit;


    public void SelectNewCharacter(CharacterUnit unit)
    {
        if(currentUnit != null)
        {
            currentUnit.Selected(false);
        }

        currentUnit = unit;
        currentUnit.Selected(true);

        characterNameText.text = currentUnit.data.characterName;
        characterDescriptionText.text = currentUnit.data.characterDescription;
        priceText.text = currentUnit.data.price.ToString();


        CharacterData data = unit.data;

        priceText.gameObject.SetActive(!data.isOwned);
        coinImage.SetActive(!data.isOwned);
        boughtText.SetActive(data.isOwned);


        if (data.isOwned)
        {
            buyButton.EdiText("Use");
        }
        else
        {
            buyButton.EdiText("Buy");
        }

    }

    public void UseSelectedCharacter()
    {
        if (currentUnit == null) return;

        if(currentUnit.data.isOwned)
        {
            //if its owned we just use it
        }
        else
        {
            //if its not then we cchecck if the player has money

            if (PlayerHandler.instance.HasCoin(currentUnit.data.price))
            {
                //he buys and there are some cool effeccts.
            }
            else
            {
                //we tell the player he cannnot
            }

        }

    }

    void CloseSelector()
    {
        selectorHolder.SetActive(false);
    }
    void OpenSelector()
    {
        selectorHolder.SetActive(true);
    }

    void ClearTargetContainer(Transform target)
    {
        for (int i = 0; i < target.childCount; i++)
        {
            Destroy(target.GetChild(0).gameObject);
        }
    }
}
