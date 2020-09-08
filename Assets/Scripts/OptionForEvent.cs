using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class OptionForEvent : ScriptableObject
{
    public string Title;
    public Enumerations.OptionType optionType=Enumerations.OptionType.Gold;
    public bool Add;
    public bool canIgnoreFight=true;
    public int value;
    public NpcInfo npc;
    
}
