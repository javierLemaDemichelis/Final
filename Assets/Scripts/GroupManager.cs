using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GroupManager : MonoBehaviour
{

    [SerializeField]
    GameObject npcPrefab;
    [SerializeField]
    bool itsPlayerGroup=false;
    [SerializeField]
    List<GameObject> members;
    [SerializeField]
    Transform[] spawPosition;
    [SerializeField]
    NpcInfo[] npcInfoList;
    private void Awake()
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateGroup(Dictionary<int,NpcInfo> npcGroupInfo) 
    {
        this.itsPlayerGroup = true;
        this.transform.tag = "HeroGroup";
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        for (int i = 0; i < 3; i++) 
        {
            if (npcGroupInfo.ContainsKey(i)) 
            {
                GameObject hero = Instantiate(npcPrefab, spawPosition[i]);
                NpcInfo temp = new NpcInfo();
                npcGroupInfo.TryGetValue(i, out temp);
                hero.GetComponent<Npc>().SetNpcInfo(temp) ;
                hero.transform.position = spawPosition[i].position;
                hero.GetComponent<Npc>().setItsPlayerGroup(true);
                members.Add(hero);
                gameManager.GetComponent<GameManagerController>().SetPlayerGroup(this.gameObject);
            }
        }
    }
    public void CreateEnemyGroup(AdventureEvents adventureEvent)
    {
        this.itsPlayerGroup = false;
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");

        if (!itsPlayerGroup)
        {
            GameObject npc;
            if (adventureEvent.npc1 != null) 
            {
                npc = Instantiate(npcPrefab, spawPosition[2]);
                npc.transform.position = spawPosition[2].position;
                npc = CreateNpc(npc,adventureEvent.npc1);
                members.Add(npc);
            }
            if (adventureEvent.npc2 != null)
            {
                npc = Instantiate(npcPrefab, spawPosition[1]);
                npc.transform.position = spawPosition[1].position;
                npc = CreateNpc(npc, adventureEvent.npc2);
                members.Add(npc);
            }
            if (adventureEvent.npc3 != null)
            {
                npc = Instantiate(npcPrefab, spawPosition[0]);
                npc.transform.position = spawPosition[0].position;
                npc = CreateNpc(npc, adventureEvent.npc3);
                members.Add(npc);
            }




            gameManager.GetComponent<GameManagerController>().SetEnemyGroup(this.gameObject);
        }
    }
    public bool GetState() 
    {
        bool someoneAlive = false;
        if (itsPlayerGroup)
        {
            foreach (GameObject member in members) 
            {
                if (member.GetComponent<Npc>().GetLifeValue() > 0) 
                {
                    someoneAlive = true;
                    break;
                }
            }
        }
        else 
        {
            foreach (GameObject member in members)
            {
                if (member.GetComponent<Npc>().GetLifeValue() > 0)
                {
                    someoneAlive = true;
                    break;
                }
            }
        }
        return someoneAlive;
    }
    public bool GetActionLeft() 
    {
        bool someonerHasAnAction = false;
        if (itsPlayerGroup)
        {
            foreach (GameObject member in members)
            {
                if (!member.GetComponent<Npc>().GetActionExpended())
                {
                    someonerHasAnAction = true;
                    break;
                }
            }
        }
        else
        {
            foreach (GameObject member in members)
            {
                if (!member.GetComponent<Npc>().GetActionExpended() )
                {
                    someonerHasAnAction = true;
                    break;
                }
            }
        }
        return someonerHasAnAction;
    }
    private GameObject CreateNpc(GameObject _npc,NpcInfo npcInfo) 
    {
        
        //switch (enemyType) 
        //{
        //    case Enumerations.EnemyType.Robber:
        _npc.GetComponent<Npc>().SetNpcInfo(npcInfo);
        //        break;
        //}
        return _npc;
                
    }
    public void Attack() 
    {
        StartCoroutine("AttackCoroutine");
    }
    public void SetActionsEnabled(bool state) 
    {
        if (itsPlayerGroup)
        {
            foreach (GameObject member in members)
            {
                member.GetComponent<Npc>().SetActionExpended(state);
            }
        }
        else
        {
            foreach (GameObject member in members)
            {
                if (!member.GetComponent<Npc>().GetActionExpended())
                {
                    member.GetComponent<Npc>().SetActionExpended(state);
                    break;
                }
            }
        }
    }
    public List<GameObject> GetMembers() 
    {
        return members;
    }

    IEnumerator AttackCoroutine() 
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Ataque iniciado");
        if (!itsPlayerGroup)
        {
            foreach (GameObject member in members)
            {
                Debug.Log("enemigo:"+members.IndexOf(member));
                Debug.Log("ActionExpended: "+ member.GetComponent<Npc>().GetActionExpended());
                if (member.GetComponent<Npc>().GetActionExpended()==false)
                {
                    Debug.Log("enemigo elegido para realizar el ataque");
                    
                    GameObject heroGroup=GameObject.FindGameObjectWithTag("HeroGroup");

                    List<GameObject> heroes=heroGroup.GetComponent<GroupManager>().GetMembers();

                    bool heroAliveFound = false;
                    while (!heroAliveFound) 
                    {

                        int randValue = Random.Range(0, heroes.Count);
                        if (heroes[randValue].GetComponent<Npc>().GetcharacterState())
                        { 
                            Debug.Log("heroe elegido para recibir el ataque");
                            int attackValue = member.GetComponent<Npc>().GetAttackValue();
                            
                            int heroeNewLife = heroes[randValue].GetComponent<Npc>().GetLifeValue()-attackValue;
                            Debug.Log("heroe ahora tiene "+heroeNewLife+" de vida");
                            heroes[randValue].GetComponent<Npc>().SetLifeValue(heroeNewLife);
                            heroes[randValue].GetComponent<Npc>().RecieveAttack();
                            member.GetComponent<Npc>().SetActionExpended(true);
                            heroAliveFound = true;

                        }
                    }
                    
                    break;
                }
            }
            GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
            gameManager.GetComponent<GameManagerController>().SetState(Enumerations.GameState.EnemyTurn);
        }
        
    }
    
}
