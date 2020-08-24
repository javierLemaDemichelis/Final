using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventContainer : MonoBehaviour
{
    [SerializeField]
    GameObject backgroundOfEvent;
    [SerializeField]
    List<AdventureEvents> adventureEvents;
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    GameObject groupContainerPrefab;
    [SerializeField]
    AdventureEvents adventureEvent;
    [SerializeField]
    GameObject enemyGroup;
    [SerializeField]
    bool startEvent = false;
    // Start is called before the first frame update
    void Start()
    {
        if (startEvent)
        {
            CreateStartEvent();
        }
        else 
        {
            CreateRandomEvent();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateRandomEvent() 
    {
        adventureEvent = adventureEvents[Random.Range(1,adventureEvents.Count-1)];
        backgroundOfEvent.GetComponent<SpriteRenderer>().sprite = adventureEvent.background;
        enemyGroup = Instantiate(groupContainerPrefab,this.transform);
        enemyGroup.transform.position = spawnPoint.position;
        enemyGroup.GetComponent<GroupManager>().CreateEnemyGroup(adventureEvent);
    }
    public void CreateStartEvent()
    {
        adventureEvent = adventureEvents[0];
        backgroundOfEvent.GetComponent<SpriteRenderer>().sprite = adventureEvent.background;
        enemyGroup = Instantiate(groupContainerPrefab, this.transform);
        enemyGroup.transform.position = spawnPoint.position;
        enemyGroup.GetComponent<GroupManager>().CreateEnemyGroup(adventureEvent);
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager.GetComponent<GameManagerController>().SetCurrentEvent(this.gameObject);
    }
    IEnumerator SelfDestruct(float time) 
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

}
