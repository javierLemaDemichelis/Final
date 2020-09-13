using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NpcInfo : ScriptableObject
{
    public string Name;
//    public string Type;
    public int OffensiveValue;
    public int DefensiveValue;
    public int HealingValue;
    public int LifeValue;
    public Sprite graphic;
    public LootInfo loot;
    public bool inanimateObject = false;
    public bool itsLooteable=true;
}
