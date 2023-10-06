using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] GameObject mainScreen;
    [SerializeField] List<GameObject> screenList = new();
    [SerializeField] TextMeshProUGUI goldText;

    private void Start()
    {
        GameHandler.instance.observer.EventMMUpdateGold += UpdatePlayerGold;
    }

    void UpdatePlayerGold(int gold)
    {
        //TERRIBLE SOLUTION BUT IT IS WHAT IT IS.
        goldText.text = "Gold: " + gold.ToString();
    }

    public void Play()
    {
        //call the next ui.
        Desactivate();
        mainScreen.SetActive(false);
        screenList[0].SetActive(true);
    }

    public void Back()
    {
        Desactivate();
        mainScreen.SetActive(true);
    }
    
    void Desactivate()
    {
        foreach (var item in screenList)
        {
            item.SetActive(false);
        }
    }


    public void QuitGame()
    {
        Debug.Log("oy");
        Application.Quit();
    }


    public void DEBUG_DELETESAVE()
    {
        GameHandler.instance.DeleteSave();
    }
}
