using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using TMPro;
public class Deck : MonoBehaviour
{
    [SerializeField]
    TextMeshPro countOfCards = null;
    [SerializeField]
    GameObject cardPrefab = null;
    //[SerializeField]
    //GameObject spawnPointForCards;
    [SerializeField]
    Transform[] positions = null;
    [SerializeField]
    public GameObject[] cardsTemp=null;
    [SerializeField]
    List<CardStock> cards;
    List<CardStock> cardsStocksRetrived=new List<CardStock>();
    List<CardInfo> cardsSorted = new List<CardInfo>();
    bool cardsRetrieved = false;
    // Start is called before the first frame update
    void Start()
    {

        RetrieveCardsFromInventory();
        countOfCards.text = cardsSorted.Count.ToString();
        OrganizeCards();
        CountCards();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void RetrieveCardsFromInventory() 
    {
        GameObject deckInformation = GameObject.FindGameObjectWithTag("DeckInformation");
        if (deckInformation != null) 
        {
            cards=deckInformation.GetComponent<DeckInformation>().GetCardInStock();
            cardsStocksRetrived = cards;
            //Debug.Log("cards "+cards.Count);
            /*
            foreach (CardStock _cardStock in cards) 
            {
                //Debug.Log("card:"+_cardStock.GetCardInfo().description+";"+ _cardStock.GetCount());
                for (int i = 0; i < _cardStock.GetCount(); i++) 
                {
                    cardsWithOutSorting.Add(_cardStock.GetCardInfo());
                }
            }*/
            int cardsQuantity = 0;
            foreach (CardStock _cardStock in cardsStocksRetrived) 
            {
                cardsQuantity += _cardStock.GetCount();
                _cardStock.SetCardsUsed(0);
            }
            Debug.Log("cards quantity:" + cardsQuantity);
            int randomStock = 0;
            //mezcla las cartas
            while (cardsSorted.Count<cardsQuantity) 
            {
                randomStock = Random.Range(0, cards.Count);
                if (cardsStocksRetrived[randomStock].GetCardsUsed() <= cardsStocksRetrived[randomStock].GetCount()) 
                {
                    cardsSorted.Add(cardsStocksRetrived[randomStock].GetCardInfo());
                    cardsStocksRetrived[randomStock].UseCard();
                }
                
            }


            //
            //Debug.Log("cards:"+cardsWithOutSorting.Count);
            cardsRetrieved = true;



            //TODO
            //SORT THE CARDS
            //cardsSorted = cardsWithOutSorting;

        }
    }
    public void OrganizeCards()
    {
        //Debug.Log("positions lenght: "+positions.Length);
        for (int i = positions.Length-1; i > -1; i--) 
        {
            if (positions[i].childCount == 0) 
            {
                bool noMoreCard = true;
                for (int j = i - 1; j > -1; j--)
                {

                    if (positions[j].childCount == 1) 
                    {
                        positions[j].GetChild(0).transform.parent = positions[i].transform;
                        positions[i].GetChild(0).GetComponent<Card>().SetContainer(positions[i].transform);
                        positions[i].GetChild(0).transform.localPosition = new Vector3(0, positions[i].GetChild(0).transform.localPosition.y, positions[i].GetChild(0).transform.localPosition.z);
                        noMoreCard = false;
                        break;
                    }
                }
                if (noMoreCard && cardsSorted.Count>0) 
                {
                    //Debug.Log("no more card in position:" + i);
                    GameObject card = Instantiate(cardPrefab, positions[i].transform);
                    card.GetComponent<Card>().SetContainer(positions[i].transform);
                    CardInfo cardInfoTemp = GetCardFromDeck();
                    card.GetComponent<Card>().SetCardInfo(cardInfoTemp);
                    //break;
                }
            }
            
        }
        
    }
    private CardInfo GetCardFromDeck() 
    {
        CardInfo cardInfoToReturn;
        cardInfoToReturn =cardsSorted.ElementAt(cardsSorted.Count-1);
        cardsSorted.RemoveAt(cardsSorted.Count-1);
        countOfCards.text = cardsSorted.Count.ToString();
        return cardInfoToReturn;
    }
    public void PutCardInDeck(CardInfo _card)
    {
       
        cardsSorted.Add(_card);
        countOfCards.text = cardsSorted.Count.ToString();
        
    }
    public int CountCards()
    {
        int countedCards = 0;
        int cardsInHand = 0;
        cardsTemp = null;
        //cardsTemp = GameObject.FindGameObjectsWithTag("Card");
        for (int i = 0; i < positions.Length; i++) 
        {
            if (positions[i].childCount>0) 
            {
                cardsInHand++;
            }
        } 
        
        //Debug.Log("cartas en mano:" + cardsInHand);
        //Debug.Log("cartas en mazo:" + cardsSorted.Count);
        countedCards = cardsSorted.Count + cardsInHand;
        return countedCards;
    }
    public void SetActiveDeck(bool state) 
    {
        this.gameObject.SetActive(state);
    }
    public bool GetActiveDeck() 
    {
        return this.gameObject.activeSelf;
    }
    public void AddCard(CardInfo _card) 
    {
    
    }
}
