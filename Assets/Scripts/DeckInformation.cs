using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeckInformation : MonoBehaviour
{
    public static DeckInformation instance;
    [SerializeField]
    GameObject initCardPack = null;
    [SerializeField]
    GameObject initHeroPack = null;
    [SerializeField]
    List<CardStock> cards = new List<CardStock>();
    Dictionary<int, NpcInfo> npcInParty=new Dictionary<int, NpcInfo>();
    private int gold = 500;
    bool initCardOpen = false;
    bool initHeroOpen = false;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }

        }
    }
    private void Start()
    {
        if (!initCardOpen) 
        {
            Instantiate(initCardPack);
            initCardOpen = true;
        }
        if (!initHeroOpen)
        {
            Instantiate(initHeroPack);
            initHeroOpen = true;
        }
    }

    public void AddCardToInventory(CardInfo card)
    {
        bool cardFound = false;
        foreach (CardStock _card in cards)
        {
            if (_card.GetCardInfo().description.Equals(card.description))
            {
                cardFound = true;
                _card.SetCount(_card.GetCount() + 1);
                //Debug.Log("se agrego una carta");
                break;
            }
        }
        if (!cardFound)
        {
            cards.Add(new CardStock(card, 1));
            //Debug.Log("se compro una nueva carta");
            //Debug.Log("cantidad de cartas:"+cards.Count);
            //Debug.Log(card.description);
        }
        //mostrar el inventario
        /*foreach (CardStock _card in cards)
        {
            Debug.Log("card:" + _card.GetCardInfo().description + ",count:" + _card.GetCount());
        }*/

    }

    public void AddHeroToGroup(NpcInfo _hero,int position) 
    {
        //agregar esta bosta a un diccionario con los participantes del grupo y su posicion
        npcInParty.Add(position, _hero);


    }
    public Dictionary<int, NpcInfo> GetNpcGroupInfo() 
    {
        return npcInParty;
    }

    //retorna una lista de los stock de cartas
    //cada stock contiene una tipo de carta y la cantidad de las mismas
    public List<CardStock> GetCardInStock() 
    {
        return this.cards;
    }
    public void AddGold(int goldToAdd)
    {
        this.gold += goldToAdd;
    }
    public void RemoveGold(int goldToAdd)
    {
        this.gold -= goldToAdd;
    }
    public int GetGold() 
    {
        return gold;
    }
}
