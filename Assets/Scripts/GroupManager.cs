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
    [SerializeField] GameObject enemyGroupToAttack;
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
        //this.transform.tag = "HeroGroup";
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
        
        foreach (GameObject member in members)
        {
            if (member.GetComponent<Npc>().GetLifeValue() > 0&& !member.GetComponent<Npc>().GetItsInanimateObject())
            {
                someoneAlive = true;
                break;
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
                if (!member.GetComponent<Npc>().GetActionExpended()&& member.GetComponent<Npc>().GetLifeValue()>0)
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
                if (!member.GetComponent<Npc>().GetActionExpended() && member.GetComponent<Npc>().GetLifeValue() > 0)
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
    public void SetActionsExpended(bool state) 
    {
        if (itsPlayerGroup)
        {
            foreach (GameObject member in members)
            {
                if (member.GetComponent<Npc>().GetLifeValue() > 0) 
                {
                    member.GetComponent<Npc>().SetActionExpended(state);
                }
                
            }
        }
        else
        {
            foreach (GameObject member in members)
            {
                if (member.GetComponent<Npc>().GetLifeValue() > 0)
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
        Debug.Log("atackCorrutine");
        yield return new WaitForSeconds(1f);
        Debug.Log("Ataque iniciado");
        
        if (!itsPlayerGroup) 
        {

            foreach (GameObject member in members) 
            {

                if (!member.GetComponent<Npc>().GetItsInanimateObject()&& member.GetComponent<Npc>().GetLifeValue()>0) 
                {
                    Debug.Log("el atacante:" + member.transform.name);
                    GameObject enemyToAttack = null;
                    enemyToAttack = ChooseEnemyToAttack();
                    if (enemyToAttack != null)
                    {
                        int attackValue = member.GetComponent<Npc>().GetAttackValue();

                        int heroeNewLife = enemyToAttack.GetComponent<Npc>().GetLifeValue() - attackValue;
                        Debug.Log("heroe ahora tiene " + heroeNewLife + " de vida");
                        enemyToAttack.GetComponent<Npc>().SetLifeValue(heroeNewLife);
                        enemyToAttack.GetComponent<Npc>().RecieveAttack();
                        member.GetComponent<Npc>().SetActionExpended(true);
                        yield return new WaitForSeconds(1f);
                    }
                }
                
            }
            Debug.Log("finalizo el turno de los enemigos");
            GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
            if (!GetState())
            {
                gameManager.GetComponent<GameManagerController>().RechargeActionPointsForPlayerGroup();
                gameManager.GetComponent<GameManagerController>().SetState(Enumerations.GameState.Loot);
            }
            else 
            {
                gameManager.GetComponent<GameManagerController>().RechargeActionPointsForPlayerGroup();
                gameManager.GetComponent<GameManagerController>().SetState(Enumerations.GameState.Check);
            }
            
        }
    }
    public GameObject ChooseEnemyToAttack() 
    {
        if (enemyGroupToAttack != null)
        {
            List<GameObject> enemyGroupMembers = enemyGroupToAttack.GetComponent<GroupManager>().GetMembers();
            List<GameObject> enemyGroupMembersAlive = new List<GameObject>();
            foreach (GameObject enemyMember in enemyGroupMembers) 
            {
                if (enemyMember.GetComponent<Npc>().GetLifeValue()>0) 
                {
                    enemyGroupMembersAlive.Add(enemyMember);
                }
            }
            //una vez tenemos los miembros vivos del grupo enemigo( grupo del player)
            int randAliveMember = 0;
            randAliveMember = Random.Range(0, enemyGroupMembersAlive.Count);
            return enemyGroupMembersAlive[randAliveMember];
        }
        else 
        {
            Debug.Log("there is no enemy group to attack");
            return null;
        }
        
    }
    public void SetEnemyGroupToAttack(GameObject _enemyGroupToAttack) 
    {
        enemyGroupToAttack = _enemyGroupToAttack;
    }
    
}
