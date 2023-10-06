using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;

    public void UpdateCoin(int currentGold, int gainedGold, int total)
    {
        coinText.text = "Coin: " + currentGold.ToString() + " / " + total.ToString();
    }

    //we are going to do effct.
    IEnumerator AddCoinProcess()
    {
        //a special effect when you gain a coin.
        yield return null;
    }


}
