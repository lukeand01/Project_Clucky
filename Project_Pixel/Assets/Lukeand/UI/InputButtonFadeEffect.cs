using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputButtonFadeEffect : MonoBehaviour
{
    InputButton input;
    [SerializeField] Image[] allEffectImages;
    bool hasChanged;
    private void Awake()
    {
        input = GetComponent<InputButton>();
    }

    private void FixedUpdate()
    {
        if (input == null) return;

        if(input.value > 0)
        {
            if (!hasChanged)
            {
                foreach (var image in allEffectImages)
                {
                    ChangeImageAlpha(image, 0.8f);
                }
                hasChanged = true;
            }

            
        }
        else
        {
            if (hasChanged)
            {
                foreach (var image in allEffectImages)
                {
                    ChangeImageAlpha(image, 0.4f);
                }
                hasChanged = false;
            }      
        }

    }

    void ChangeImageAlpha(Image targetImage, float alpha)
    {
        var a = targetImage.color;
        a.a = alpha;
        targetImage.color = a;
    }

}
