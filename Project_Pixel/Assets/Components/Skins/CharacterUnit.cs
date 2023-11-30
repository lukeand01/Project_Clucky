using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterUnit : ButtonBase
{
    public CharacterData data {  get; private set; }    
    [SerializeField] Image portrait;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] GameObject stickerOwned;
    [SerializeField] GameObject stickerSelected;
    [SerializeField] GameObject selected;

    public void SetUp(CharacterData data)
    {
        this.data = data;
        UpdateUI();
    }

    void UpdateUI()
    {

        if (data == null)
        {
            Debug.Log("data");
            return;
        }

        portrait.sprite = data.icon;
        coinText.text = data.price.ToString();
        
        ControlStickerSelected(data.isSelected);
        ControlStickerOwned(data.isOwned);
    }


    public void ControlStickerOwned(bool choice)
    {
        stickerOwned.SetActive(choice);
    }
    public void ControlStickerSelected(bool choice)
    { 
    stickerSelected.SetActive(choice);
    }

    public void Selected(bool choice)
    {
        selected.SetActive(choice);
    }


    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
    }
}
