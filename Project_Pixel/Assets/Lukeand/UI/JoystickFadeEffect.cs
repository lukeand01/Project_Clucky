using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickFadeEffect : MonoBehaviour
{
    FixedJoystick joystick;

    [SerializeField] Image outerCircle;
    [SerializeField] Image innerCircle;
    [SerializeField] Image graphicalCircle;


    private void Awake()
    {
        joystick = GetComponent<FixedJoystick>();   
    }


    private void FixedUpdate()
    {
        if (joystick == null) return;


        if(joystick.Direction == Vector2.zero )
        {
            ChangeImageAlpha(outerCircle, 0.4f);
            ChangeImageAlpha(innerCircle, 0.4f);
            ChangeImageAlpha(graphicalCircle, 0.4f);
        }
        else
        {
            ChangeImageAlpha(outerCircle, 0.8f);
            ChangeImageAlpha(innerCircle, 0.8f);
            ChangeImageAlpha(graphicalCircle, 0.8f);
        }

    }

    void ChangeImageAlpha(Image targetImage, float alpha)
    {
        var a = targetImage.color;
        a.a = alpha;
        targetImage.color = a;
    }

}
