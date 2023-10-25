using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    //open this feella

    bool isOpen;
    [SerializeField] float speed;
    [SerializeField] float height;
    public void OpenGate()
    {
        if (!isOpen)
        {
            StopAllCoroutines();
            StartCoroutine(OpenProcess());
        }

    }

    IEnumerator OpenProcess()
    {
        isOpen = true;

        Vector3 openOffset = new Vector3(0, height, 0);
        Vector3 originalPos = transform.position;

        while (transform.position != originalPos + openOffset)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPos + openOffset, speed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }

    }



    public void CloseGate()
    {      
      StopAllCoroutines();
      StartCoroutine(CloseProcess());              
    }

    IEnumerator CloseProcess()
    {
       

        Vector3 openOffset = new Vector3(0, -height, 0);
        Vector3 originalPos = transform.position;

        while (transform.position != originalPos + openOffset)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPos + openOffset, speed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        isOpen = false;
    }

}
