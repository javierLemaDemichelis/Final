using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PredefinedTimeline : ScriptableObject
{
    [SerializeField] TerrainEvents[] terrains;
    [SerializeField] AdventureEvents[] linearAdventureEvents;
}
