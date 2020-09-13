using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManagerController : MonoBehaviour
{
    [SerializeField] TextMeshPro stateText = null;
    [SerializeField] Enumerations.GameState state = Enumerations.GameState.Adventure;
    [SerializeField] GameObject pointer = null;
    [SerializeField] GameObject cardForAction = null;
    [SerializeField] GameObject heroForAction = null;
    [SerializeField] GameObject actionObjective = null;
    [SerializeField] GameObject deck;
    [SerializeField] GameObject groupPrefab;
    [SerializeField] Transform SpawnpointforPlayerGroup;
    [SerializeField] Transform positionToDeleteEvent;
    [SerializeField] GameObject playerGroup;
    [SerializeField] GameObject enemyGroup;
    [SerializeField] GameObject eventContainerPrefab;
    [SerializeField] GameObject currentEvent;
    [SerializeField] GameObject StartButton;
    [SerializeField] Transform showedDeckPosition;
    [SerializeField] Transform hidedDeckPosition;
    [SerializeField] GameObject timelineObject;
    private void Awake()
    {
        pointer = GameObject.FindGameObjectWithTag("Pointer");
        retrieveGroupFromInventory();
        stateText.text = "State: " + state.ToString();
        

    }
    private void Start()
    {
        AdventureEvents adventureEvent= timelineObject.GetComponent<TimelineClass>().GetCurrentEvent();
        currentEvent = Instantiate(eventContainerPrefab, this.transform.parent.transform);
        currentEvent.GetComponent<EventContainer>().InstantiateEvent(adventureEvent);
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
                
                pointer.GetComponent<Pointer>().SetState(Enumerations.GameState.Drag);
                break;
            case Enumerations.GameState.Action:
                pointer.GetComponent<Pointer>().SetState(Enumerations.GameState.Action);
                break;
            case Enumerations.GameState.EnemyTurn:
                enemyGroup.GetComponent<GroupManager>().StartCoroutine("AttackCoroutine");
                break;
            case Enumerations.GameState.Loot:
                Debug.Log("loot corrutine called");
                StartCoroutine("SearchLoot", 1);
                break;
            case Enumerations.GameState.Check:
                Debug.Log("check state");
                if (CheckForEndGame())
                {
                    SetState(Enumerations.GameState.Endgame);

                }
                else 
                {
                    if (playerGroup.GetComponent<GroupManager>().GetActionLeft()) 
                    {
                        SetState(Enumerations.GameState.Drag);
                    }
                    else 
                    {
                        SetState(Enumerations.GameState.EnemyTurn);
                    }
                    
                }
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
                actionObjective.GetComponent<Npc>().SetLifeValue(actionObjective.GetComponent<Npc>().GetLifeValue() - valueToDealToObjective);
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
        SetState(Enumerations.GameState.Check);

    }
    private bool CheckForEndGame() 
    {
        bool someoneAlive=playerGroup.GetComponent<GroupManager>().GetState();
        int playerHasCardLeft = deck.GetComponent<Deck>().CountCards();
        if (someoneAlive && playerHasCardLeft > 0)
        {
            return false;
        }
        else 
        {
            return true;
        }
    }
    public void EndTurn()
    {
        Debug.Log("pasar turno");
        enemyGroup.GetComponent<GroupManager>().SetActionsExpended(false);
        SetState(Enumerations.GameState.EnemyTurn);
        

    }
    public void RechargeActionPointsForPlayerGroup() 
    {
        playerGroup.GetComponent<GroupManager>().SetActionsExpended(false);
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
        playerGroup.transform.tag = "HeroGroup";
        playerGroup.transform.position = SpawnpointforPlayerGroup.position + new Vector3(-10, 0, 0);
        //-2.63744f
        LeanTween.moveX(playerGroup, SpawnpointforPlayerGroup.transform.position.x, 3);

    }

    public void CreateNewEvent()
    {
        Debug.Log("Create new event");
        //currentEvent = Instantiate(eventContainerPrefab, GameObject.FindGameObjectWithTag("GameContainer").transform);
        if (currentEvent != null)
        {
            LeanTween.moveX(currentEvent, positionToDeleteEvent.transform.position.x, 3);
            currentEvent.GetComponent<EventContainer>().StartCoroutine("SelfDestruct", 3.2f);
        }
        currentEvent = Instantiate(eventContainerPrefab, this.transform.parent.transform);
        Debug.Log("objeto event container creado");
        timelineObject.GetComponent<TimelineClass>().NextEvent();
        Debug.Log("next event ejecutado");
        AdventureEvents adventureEvent = timelineObject.GetComponent<TimelineClass>().GetCurrentEvent();
        Debug.Log("nuevo evento recibido");
        currentEvent.GetComponent<EventContainer>().InstantiateEvent(adventureEvent);
        Debug.Log("nuevo evento inicializado");
        currentEvent.transform.localPosition = new Vector3(20.48f, 0, 0);
        LeanTween.moveX(currentEvent, this.transform.position.x, 3);
        Debug.Log("desplegar opciones");
        currentEvent.GetComponent<EventContainer>().StartCoroutine("ShowTextOptions", 3);
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
        GameObject transitionManager = GameObject.FindGameObjectWithTag("TransitionManager");
        
        GameObject deckInformation = GameObject.FindGameObjectWithTag("DeckInformation");
        Dictionary<int, NpcInfo> npcGroupinfo=deckInformation.GetComponent<DeckInformation>().GetNpcGroupInfo();
        
        transitionManager.GetComponent<TransitionManager>().StartFadeIn();
        CreatePlayerGroup(npcGroupinfo);
        //deck.GetComponent<Deck>().SetActiveDeck(false);

    }

    //deck behavior
    IEnumerator ShowHideDeck(bool state) 
    {
        if (state) 
        {
            LeanTween.moveY(deck, showedDeckPosition.position.y, 0.3f);
        }
        else 
        {
            LeanTween.moveY(deck, hidedDeckPosition.position.y, 0.3f);
        }
        
        yield return new WaitForSeconds(1.0f);
        //activar el puntero

        
    }
    //corrutinas comunes
    IEnumerator SearchLoot(float time) 
    {
        List<GameObject> members = enemyGroup.GetComponent<GroupManager>().GetMembers();
        foreach (GameObject member in members)
        {
            if (member.GetComponent<Npc>().GetItsLooteable())
            {
                LootInfo loot = member.GetComponent<Npc>().GetLoot();
                if (loot != null) 
                {
                    int gold = 0;
                    gold = Random.Range(loot.minGold, loot.maxGold);

                    GameObject deckInformation = GameObject.FindGameObjectWithTag("DeckInformation");
                    deckInformation.GetComponent<DeckInformation>().AddGold(gold);
                    if (loot.cardsForRetrieve.Length > 0)
                    {
                        CardInfo cardInfoFromLoot;
                        cardInfoFromLoot = loot.cardsForRetrieve[Random.Range(1, loot.cardsForRetrieve.Length)];
                        deckInformation.GetComponent<DeckInformation>().AddCardToInventory(cardInfoFromLoot);
                        deck.GetComponent<Deck>().PutCardInDeck(cardInfoFromLoot);
                        Debug.Log("se adhirio: " + cardInfoFromLoot.description);
                    }
                }
                
            }
        }
        yield return new WaitForSeconds(time);
        StartCoroutine("FleeOption",3);
    }

    //deal with options
    IEnumerator AttackOption(float time) 
    {
        yield return new WaitForSeconds(time);
        playerGroup.GetComponent<GroupManager>().SetActionsExpended(false);
        currentEvent.GetComponent<EventContainer>().GetGroupFromEvent().GetComponent<GroupManager>().SetEnemyGroupToAttack(playerGroup);
        SetState(Enumerations.GameState.Drag);
        StartCoroutine("ShowHideDeck",true);
    }
    IEnumerator FleeOption(float time)
    {
        yield return new WaitForSeconds(time);
        CreateNewEvent();
        StartCoroutine("ShowHideDeck", false);

    }
    IEnumerator LootOption(float time)
    {
        yield return new WaitForSeconds(time);
        SetState(Enumerations.GameState.Loot);
        StartCoroutine("ShowHideDeck", false);
    }
    IEnumerator npcOption(float time)
    {
        yield return new WaitForSeconds(time);
    }
    IEnumerator IgnoreOption(float time)
    {
        yield return new WaitForSeconds(time);
        CreateNewEvent();
        StartCoroutine("ShowHideDeck", false);
    }
    IEnumerator StartOption(float time) 
    {
        yield return new WaitForSeconds(time);
        CreateNewEvent();
        StartCoroutine("ShowHideDeck", false);
    }
}
