using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventContainer : MonoBehaviour
{
    [SerializeField] GameObject backgroundOfEvent;
    [SerializeField] List<AdventureEvents> adventureEvents;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject groupContainerPrefab;
    [SerializeField] AdventureEvents adventureEvent;
    [SerializeField] GameObject enemyGroup;
    [SerializeField] bool startEvent = false;
    [SerializeField] GameObject eventChat;
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
        adventureEvent = adventureEvents[Random.Range(1,adventureEvents.Count)];
        backgroundOfEvent.GetComponent<SpriteRenderer>().sprite = adventureEvent.background;
        enemyGroup = Instantiate(groupContainerPrefab,this.transform);
        enemyGroup.transform.position = spawnPoint.position;
        enemyGroup.GetComponent<GroupManager>().CreateEnemyGroup(adventureEvent);
        eventChat.GetComponent<EventChat>().InitializeEventInfo(adventureEvent,this.GetComponent<EventContainer>());
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
    IEnumerator ShowTextOptions(float time) 
    {
        yield return new WaitForSeconds(time);
        ShowOptions();
    }
    private void ShowOptions() 
    {
        EventChat eventChatOptions = eventChat.GetComponent<EventChat>();
        eventChatOptions.HideEventChat(false);
        
    }
    public void DealWithOption(OptionForEvent _option) 
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        Debug.Log("option selected:" + _option.Title);
        switch (_option.optionType) 
        {
            case Enumerations.OptionType.Gold:
                if (_option.Add) 
                {
                    //add money
                    GameObject deckInfo = GameObject.FindGameObjectWithTag("DeckInformation");
                    deckInfo.GetComponent<DeckInformation>().AddGold(_option.value);
                }
                else 
                {
                    //get moneyAvailable
                    GameObject deckInfo = GameObject.FindGameObjectWithTag("DeckInformation");
                    int money=0;
                    money = deckInfo.GetComponent<DeckInformation>().GetGold();
                    if (_option.value > money)
                    {
                        if (_option.canIgnoreFight)
                        {
                            //keep walking

                            gameManager.GetComponent<GameManagerController>().StartCoroutine("FleeOption", 2);
                        }
                        else
                        {
                            //fight

                            gameManager.GetComponent<GameManagerController>().StartCoroutine("AttackOption", 2);
                        }
                    }
                    else 
                    {
                        deckInfo.GetComponent<DeckInformation>().RemoveGold(_option.value);
                        gameManager.GetComponent<GameManagerController>().StartCoroutine("FleeOption", 2);
                    }
                }
                break;
            case Enumerations.OptionType.Attack:

                gameManager.GetComponent<GameManagerController>().StartCoroutine("AttackOption", 2);
                break;
            case Enumerations.OptionType.Loot:
                gameManager.GetComponent<GameManagerController>().StartCoroutine("LootOption", 2);
                break;
            case Enumerations.OptionType.Npc:
                if (_option.Add) 
                {
                    
                }
                else 
                {
                    
                }
                gameManager.GetComponent<GameManagerController>().StartCoroutine("FleeOption", 2);
                break;
            case Enumerations.OptionType.Ignore:
                gameManager.GetComponent<GameManagerController>().StartCoroutine("IgnoreOption", 2);
                break;
            case Enumerations.OptionType.Flee:
                gameManager.GetComponent<GameManagerController>().StartCoroutine("FleeOption", 2);
                break;
        }
        eventChat.SetActive(false);


    }

}
