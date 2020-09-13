using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineClass :MonoBehaviour
{
    [SerializeField] PredefinedTimeline timelineWithEvents;
    [SerializeField] TerrainEvents[] terrainEvents;
    List<AdventureEvents> adventureEvents = new List<AdventureEvents>();
    int index = 0;
    int terrainIndex = 0;
    int numberOfEventsInTerrain = 0;
    AdventureEvents currentEvent;
    
    private void Awake()
    {
        for (int i = 0; i < timelineWithEvents.linearAdventureEvents.Length; i++)
        {
            adventureEvents.Add(timelineWithEvents.linearAdventureEvents[i]);
        }
        if (adventureEvents.Count == 0)
        {
            adventureEvents.Add(terrainEvents[0].adventureEventForTerrain[0]);
        }
        currentEvent = adventureEvents[0];
    }
    public void NextEvent() 
    {
        index++;
        if (adventureEvents.Count <index)
        {
            //si el evento siguiente esta vacio
            int randEventFromTerrain = 0;
            if (numberOfEventsInTerrain < terrainEvents[terrainIndex].maxLim)
            {
                //si el numero de eventos dentro de un terreno NO EXCEDE el limite permitido
                //creamos otro evento elegido de los posibles dentro del terreno actual
                numberOfEventsInTerrain++;
                randEventFromTerrain = Random.Range(0, terrainEvents[terrainIndex].adventureEventForTerrain.Length);
                adventureEvents.Add(terrainEvents[terrainIndex].adventureEventForTerrain[randEventFromTerrain]);
            }
            else
            {
                //si el numero de eventos dentro de un terreno EXCEDE el limite permitido
                //elegimos otro terreno al azar y agregamos un evento de dicho terreno
                terrainIndex = Random.Range(1, terrainEvents.Length);
                numberOfEventsInTerrain = 0;
                randEventFromTerrain = Random.Range(0, terrainEvents[terrainIndex].adventureEventForTerrain.Length);
                adventureEvents.Add(terrainEvents[terrainIndex].adventureEventForTerrain[randEventFromTerrain]);
            }
            currentEvent = adventureEvents[index];
        }
        else 
        {

            int randEventFromTerrain = 0;
            terrainIndex = Random.Range(1, terrainEvents.Length);
            numberOfEventsInTerrain = 0;
            randEventFromTerrain = Random.Range(0, terrainEvents[terrainIndex].adventureEventForTerrain.Length);
            adventureEvents.Add(terrainEvents[terrainIndex].adventureEventForTerrain[randEventFromTerrain]);
            currentEvent = adventureEvents[index];
        }
    }

    public AdventureEvents GetCurrentEvent() 
    {
        return currentEvent;
    }
}
