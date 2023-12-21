using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    GameObject holder;

    private void Awake()
    {
        holder = transform.GetChild(0).gameObject;
    }

    public void Control()
    {
        if (holder.activeInHierarchy)
        {
            holder.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            holder.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Force(bool choice)
    {
        holder.SetActive(choice);
        if (choice)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        Control();
    }

    public void RestartLevel()
    {
        Control();
        GameHandler.instance.loader.ResetScene();
    }



    public void Quit()
    {
        Debug.Log("quit");
        Control();
        GameHandler.instance.loader.ChangeScene(0);
    }

    public void Settings()
    {
        UIHolder.instance.settings.Open();
    }

}
