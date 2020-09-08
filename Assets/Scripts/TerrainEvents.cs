using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TerrainEvents : ScriptableObject
{
    public AdventureEvents[] adventureEventForTerrain;
    public int minLim = 5;
    public int maxLim = 15;
    public GameObject backgroundScene;

}
