using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardInfo : ScriptableObject
{
    public Enumerations.CardAction action;
    public string description;
    public Sprite graphic;
    public int value;

    public string GetDescriptionOfCard() 
    {
        return description;
    }
}
