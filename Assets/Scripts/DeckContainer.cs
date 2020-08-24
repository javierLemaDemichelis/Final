using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckContainer : MonoBehaviour
{
    [SerializeField]
    Transform initSpawnPoint = null;
    [SerializeField]
    GameObject cardsContainer = null;
    [SerializeField]
    float offset = 0;
    bool cardsShowed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!cardsShowed) 
        {
            GameObject deckInformation = GameObject.FindGameObjectWithTag("DeckInformation");
            if (deckInformation != null) 
            {
                ShowCards(deckInformation.GetComponent<DeckInformation>().GetCardInStock());
                cardsShowed = true;
            }
        }
    }
    public void ShowCards(List<CardStock> cards) 
    {
        foreach (CardStock _card in cards)
        {
            GameObject cardContainer;
            cardContainer = Instantiate(cardsContainer,this.gameObject.transform);
            //Debug.Log("cantidad de cartas:"+_card.GetCount());
            cardContainer.GetComponent<CardsContainer>().SetCardInfo(_card);
            cardContainer.transform.localPosition = initSpawnPoint.transform.localPosition+new Vector3(offset,0,0);
            offset += 3f;
        }
    }

}
