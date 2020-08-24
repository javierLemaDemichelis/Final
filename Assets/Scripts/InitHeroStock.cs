using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitHeroStock : MonoBehaviour
{
    [SerializeField]
    NpcInfo[] npcTypes = null;
    // Start is called before the first frame update
    void Start()
    {
        AddHero(npcTypes[0],0);
        AddHero(npcTypes[0],2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddHero(NpcInfo _hero,int position)
    {
        GameObject inventory = GameObject.FindGameObjectWithTag("DeckInformation");
        if (inventory != null)
        {
            Debug.Log("adhirio un hero al grupo");
            inventory.GetComponent<DeckInformation>().AddHeroToGroup(_hero,position);
        }
    }
}
