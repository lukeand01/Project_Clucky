using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectForJumpButton : MonoBehaviour
{
    [SerializeField] GameObject upImage;
    [SerializeField] GameObject downImage;
    [SerializeField] TextMeshProUGUI inputText;
    

    public void EffectCanJumpDown(bool isPressingDown)
    {

        downImage.SetActive(isPressingDown);
        upImage.SetActive(!isPressingDown);
       
    }



}
