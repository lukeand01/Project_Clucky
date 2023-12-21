using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TutorialData : ScriptableObject
{
    public Sprite tutorialSprite;
    [TextArea]public string tutorialDescription;
}
