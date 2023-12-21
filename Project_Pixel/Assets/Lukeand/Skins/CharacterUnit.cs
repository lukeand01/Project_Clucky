using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterUnit : ButtonBase
{
    public CharacterData data {  get; private set; }    
    CharacterUI handler;
    [SerializeField] Image portrait;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] GameObject stickerOwned;
    [SerializeField] GameObject stickerSelected;
    [SerializeField] GameObject selected;

    public void SetUp(CharacterData data, CharacterUI handler)
    {
        this.data = data;
        this.handler = handler;
        UpdateUI();
    }

    void UpdateUI()
    {

        if (data == null)
        {
            return;
        }

        portrait.sprite = data.icon;
        coinText.text = data.price.ToString();
        
        ControlStickerSelected(data.isSelected);
        ControlStickerOwned(data.isOwned);

        selected.SetActive(false);
        selected.transform.localScale = Vector3.one;
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

        StopAllCoroutines();

        if(choice)
        {
            StartCoroutine(SelectProcess());
        }
        else
        {
            StartCoroutine(UnselectProcess());
        }


    }


    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        handler.SelectNewCharacter(this);
    }


    IEnumerator SelectProcess()
    {
        while(selected.transform.localScale.x < 1.15f)
        {
            selected.transform.localScale += new Vector3(0.01f, 0.01f, 0);
            yield return new WaitForSeconds(0.01f);
        }


    }
    IEnumerator UnselectProcess()
    {
        while (selected.transform.localScale.x > 1)
        {
            selected.transform.localScale -= new Vector3(0.01f, 0.01f, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

}
