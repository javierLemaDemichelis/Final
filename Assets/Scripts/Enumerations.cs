using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enumerations 
{
    public enum GameState
    {
        Drag, Action,Adventure,EnemyTurn, Loot,Check, Endgame
    }
    public enum CardState
    {
        Drag,Action
    }
    public enum CardAction 
    {
        Attack,Defence,Heal
    }
    public enum Interactuable
    {
        Card,/* Hero,*/ Npc
    }
    public enum OptionType 
    {
        Gold,AttackPoint,DefensePoint,HealthPoints,Npc,Attack,Flee,Loot,Ignore,Start
    }

}
