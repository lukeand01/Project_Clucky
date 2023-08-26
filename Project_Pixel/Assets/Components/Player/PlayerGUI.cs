using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;

    public void UpdateCoin(int coinValue)
    {
        coinText.text = "Coin: " + coinValue.ToString();
    }

}
