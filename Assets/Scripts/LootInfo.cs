using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LootInfo : ScriptableObject
{
    public int minGold = 0;
    public int maxGold = 10;
    public CardInfo[] cardsForRetrieve;
}
