using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSign : MonoBehaviour
{
    //when the player interacts it opens the tutorial sign.

    public TutorialData data;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UIHolder.instance.tutorial.StartTutorial(data);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        UIHolder.instance.tutorial.StopTutorial();
    }


}
