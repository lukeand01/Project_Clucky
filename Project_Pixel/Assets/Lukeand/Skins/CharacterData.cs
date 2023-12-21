using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    //this is a paid product. this holds a price for the product in in game ccurrecny gold, graphicc and possible other variables. 
    //it might have different animation

    public int price;
    public GameObject graphicHolder; //this graphic holder is an object that holds the sprite and its animation
    public Sprite icon;
    public string characterName;
    [TextArea]public string characterDescription;


    public bool isOwned;
    public bool isSelected;
}
