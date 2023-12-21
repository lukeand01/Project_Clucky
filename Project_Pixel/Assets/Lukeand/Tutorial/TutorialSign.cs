using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSign : MonoBehaviour
{
    //when the player interacts it opens the tutorial sign.

    public TutorialData data;
    [SerializeField] AudioClip sfx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        UIHolder.instance.tutorial.StartTutorial(data);

        GameHandler.instance.sound.CreateSFX(sfx, transform);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        UIHolder.instance.tutorial.StopTutorial();
    }


}
