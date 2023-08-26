using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] GameObject mainScreen;
    [SerializeField] List<GameObject> screenList = new();

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


        }
