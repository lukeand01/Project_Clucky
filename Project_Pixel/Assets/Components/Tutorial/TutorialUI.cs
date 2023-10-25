using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    GameObject holder;

    [SerializeField] TutorialData data;

    [SerializeField] GameObject background;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Image icon;

    Vector3 originalPos;

    private void Awake()
    {
        holder = transform.GetChild(0).gameObject;

        
    }

    private void Start()
    {
        originalPos = background.transform.localPosition;


    }

    


    public void StartTutorial(TutorialData data)
    {
        descriptionText.text = data.tutorialDescription;

        if (data.tutorialSprite != null)
        {
            icon.gameObject.SetActive(true);
            icon.sprite = data.tutorialSprite;
        }
        else
        {
            icon.gameObject.SetActive(false);
        }

        StopAllCoroutines();
        StartCoroutine(OpenProcess());
    }

    [ContextMenu("STOP")]
    public void StopTutorial()
    {
        StopAllCoroutines();
        StartCoroutine(CloseProcess());
    }

    IEnumerator OpenProcess()
    {

        
        while (originalPos.y - 120 < background.transform.localPosition.y)
        {

            background.transform.localPosition += new Vector3(0,-1.5f, 0);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        

    }
    IEnumerator CloseProcess()
    {
        while (originalPos.y > background.transform.localPosition.y)
        {
            background.transform.localPosition += new Vector3(0, 1.5f, 0);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

}
