
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryUI : MonoBehaviour
{
    GameObject holder;
    [SerializeField] TextMeshProUGUI coinText;

    private void Awake()
    {
        holder = transform.GetChild(0).gameObject;
    }

    public void StartVictoryUI()
    {
        holder.SetActive(true);
     

        

    }


    public void StopVictoryUI()
    {
        holder.SetActive(false);
    }

    public void UpdateCoin(int current, int total)
    {
        coinText.text = "Coin: " + current + " / " + total;
    }


    IEnumerator VictoryProcess()
    {
        yield return null;
    }

    public void NextStage()
    {

        GameHandler.instance.ProgressToNextStage();
    }

    public void RetryStage()
    {
        GameHandler.instance.loader.ResetScene();
    }

    public void ReturnToMainMenu()
    {
        GameHandler.instance.loader.ChangeScene(0);
    }


}
