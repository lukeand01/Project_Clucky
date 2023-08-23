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


    public void Resume()
    {
        Control();
    }

    public void RestartLevel()
    {

    }



    public void Quit()
    {

    }

    public void Settings()
    {

    }

}
