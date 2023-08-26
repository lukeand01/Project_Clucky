using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathUI : MonoBehaviour
{
    GameObject holder;

    private void Awake()
    {
        holder = transform.GetChild(0).gameObject;
    }


    public void StartDeathUI()
    {
        holder.SetActive(true);
    }
    public void StopDeathUI()
    {
        holder.SetActive(false);
    }


    IEnumerator DeathProcess()
    {
        yield return null;
    }

    public void RetryStage()
    {
        //spawn all pieces back to thee places.
        GameHandler.instance.loader.ResetScene();
    }

    public void ReturnToMainMenu()
    {
        GameHandler.instance.loader.ChangeScene(0);
    }

}
