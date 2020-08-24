using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManagerController : MonoBehaviour
{
    [SerializeField]    TextMeshPro stateText=null;
    [SerializeField]    Enumerations.GameState state = Enumerations.GameState.Adventure;
    [SerializeField]    GameObject pointer = null;
    [SerializeField]    GameObject cardForAction = null;
    [SerializeField]    GameObject heroForAction = null;
    [SerializeField]    GameObject actionObjective = null;
    [SerializeField]    GameObject deck;
    [SerializeField]    GameObject groupPrefab;
    [SerializeField]    Transform SpawnpointforPlayerGroup;
    [SerializeField]    Transform positionToDeleteEvent;
    [SerializeField]    GameObject playerGroup;
    [SerializeField]    GameObject enemyGroup;
    [SerializeField]    GameObject eventContainerPrefab;
    [SerializeField]    GameObject currentEvent;

    private void Awake()
    {
        pointer = GameObject.FindGameObjectWithTag("Pointer");
        retrieveGroupFromInventory();
        stateText.text = "State: "+state.ToString();
        
    }
    public Enumerations.GameState GetState() 
    {
        return state;
    }
    public void SetState(Enumerations.GameState newState) 
    {
        this.state = newState;
        stateText.text = "State: " + state.ToString();
        switch (state)
        {
            case Enumerations.GameState.Drag:
                if (playerGroup.GetComponent<GroupManager>().GetState())
                {
                    pointer.GetComponent<Pointer>().SetState(Enumerations.GameState.Drag);
                }
                else 
                {
                    SetState(Enumerations.GameState.Endgame);
                }
                

                
                break;
            case Enumerations.GameState.Action:
                pointer.GetComponent<Pointer>().SetState(Enumerations.GameState.Action);
                break;
            case Enumerations.GameState.EnemyTurn:

                if (enemyGroup.GetComponent<GroupManager>().GetState()&& enemyGroup.GetComponent<GroupManager>().GetActionLeft())
                {
                    Debug.Log("ataque enemigo en progreso");
                    enemyGroup.GetComponent<GroupManager>().Attack();

                }
                else 
                {
                    SetState(Enumerations.GameState.Drag);
                    playerGroup.GetComponent<GroupManager>().SetActionsEnabled(false);
                    pointer.GetComponent<Pointer>().SetModeOfSensor(0);
                }
                break;
            case Enumerations.GameState.Loot:
                break;
            case Enumerations.GameState.Endgame:
                pointer.GetComponent<Pointer>().SetState(Enumerations.GameState.Endgame);
                break;
        }
    }
    public void SetCardForAction(GameObject _card) 
    {
        cardForAction = _card;
    }
    public void SetHeroForAction(GameObject _hero) 
    {
        heroForAction = _hero;
    }
    public void SetObjectiveForAction(GameObject _objective) 
    {
        actionObjective = _objective;
        CompleteAction();
    }
    public void CompleteAction()
    {
        int valueToDealToObjective = 0;
        CardInfo _cardInfoTemp = null;
       
        _cardInfoTemp = cardForAction.GetComponent<Card>().GetCardInfo();
        

        switch (_cardInfoTemp.action)
        {
            case Enumerations.CardAction.Attack:
               
                valueToDealToObjective = _cardInfoTemp.value + heroForAction.GetComponent<Npc>().GetAttackValue();
                actionObjective.GetComponent<Npc>().SetLifeValue(actionObjective.GetComponent<Npc>().GetLifeValue()-valueToDealToObjective);
                actionObjective.GetComponent<Npc>().RecieveAttack();
                break;
            case Enumerations.CardAction.Defence:
                break;
            case Enumerations.CardAction.Heal:
                break;
        }
        heroForAction.GetComponent<Npc>().SetActionExpended(true);
        Destroy(cardForAction);
        actionObjective = null;
        heroForAction = null;
        deck.GetComponent<Deck>().OrganizeCards();
        int countOfCardAvalible = deck.GetComponent<Deck>().CountCards();
        bool playerGroupAlive = playerGroup.GetComponent<GroupManager>().GetState();
        bool enemyGroupAlive = enemyGroup.GetComponent<GroupManager>().GetState();
        bool playerGroupHasActionsLeft= playerGroup.GetComponent<GroupManager>().GetActionLeft();
        //Debug.Log("Cartas restantes:"+temp);
        if (countOfCardAvalible > 0&&playerGroupAlive)
        {
            if (enemyGroupAlive) 
            {
                if (playerGroupHasActionsLeft)
                {
                    SetState(Enumerations.GameState.Drag);
                }
                else 
                {
                    enemyGroup.GetComponent<GroupManager>().SetActionsEnabled(false);
                    SetState(Enumerations.GameState.EnemyTurn);
                }
            }
            else 
            {
                SetState(Enumerations.GameState.Adventure);
                pointer.GetComponent<Pointer>().SetState(Enumerations.GameState.Adventure);
                StartCoroutine("NewEvent");

            }
            
        }
        else 
        {
            SetState(Enumerations.GameState.Endgame);
            pointer.GetComponent<Pointer>().SetState(Enumerations.GameState.Endgame);
            Debug.Log("end game");
        }
        
        
    }

    public void EndTurn() 
    {
        
    }

    public void ReturnCard() 
    {
        cardForAction.GetComponent<Card>().ReturnToContainer();
    }
    public void SetPlayerGroup(GameObject _playerGroup)
    {
        this.playerGroup = _playerGroup;
    }
    public void SetEnemyGroup(GameObject _enemyGroup) 
    {
        this.enemyGroup = _enemyGroup;
        SetState(Enumerations.GameState.Drag);
    }
    public void CreatePlayerGroup(Dictionary<int, NpcInfo> _npcGroupinfo) 
    {
        playerGroup = Instantiate(groupPrefab, GameObject.FindGameObjectWithTag("GameContainer").transform);
        playerGroup.GetComponent<GroupManager>().CreateGroup(_npcGroupinfo);
        //playerGroup.transform.position = SpawnpointforPlayerGroup.position;
        playerGroup.transform.position = SpawnpointforPlayerGroup.position+new Vector3(-10,0,0);
        //-2.63744f
        LeanTween.moveX(playerGroup, SpawnpointforPlayerGroup.transform.position.x, 3);
    }

    public void CreateNewEvent() 
    {
        //currentEvent = Instantiate(eventContainerPrefab, GameObject.FindGameObjectWithTag("GameContainer").transform);
        if (currentEvent != null)
        {
            LeanTween.moveX(currentEvent, positionToDeleteEvent.transform.position.x, 3);
            currentEvent.GetComponent<EventContainer>().StartCoroutine("SelfDestruct", 3.1f);
        }
        currentEvent = Instantiate(eventContainerPrefab, GameObject.FindGameObjectWithTag("GameContainer").transform);
        currentEvent.transform.localPosition = new Vector3(20.8f, 0, 0);
        LeanTween.moveX(currentEvent, this.transform.position.x, 3);
    }
    public void StartAdventure() 
    {
        if (currentEvent != null) 
        {
            LeanTween.moveX(currentEvent,positionToDeleteEvent.transform.position.x,3);
            currentEvent.GetComponent<EventContainer>().StartCoroutine("SelfDestruct", 3.1f);
        }
        currentEvent = Instantiate(eventContainerPrefab, GameObject.FindGameObjectWithTag("GameContainer").transform);
        currentEvent.transform.localPosition = new Vector3(20.8f,0,0);
        LeanTween.moveX(currentEvent, this.transform.position.x, 3);
        //currentEvent = Instantiate(eventContainerPrefab,transform.position,Quaternion.identity);
    }
    public void SetCurrentEvent(GameObject currentEvent)
    {
        this.currentEvent = currentEvent;
    }
    IEnumerator NewEvent() 
    {
        yield return new WaitForSeconds(1f);
        Destroy(enemyGroup.gameObject);
        enemyGroup = null;
        CreateNewEvent();
    }
    public void retrieveGroupFromInventory() 
    {
        GameObject deckInformation = GameObject.FindGameObjectWithTag("DeckInformation");
        Dictionary<int, NpcInfo> npcGroupinfo=deckInformation.GetComponent<DeckInformation>().GetNpcGroupInfo();
        CreatePlayerGroup(npcGroupinfo);
    }
    
}
