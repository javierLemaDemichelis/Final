using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AdventureEvents : ScriptableObject
{
    public GameObject background=null;
    public NpcInfo npc1;
    public NpcInfo npc2;
    public NpcInfo npc3;

    public string eventText0 = "";
    public OptionForEvent[] options;


}
