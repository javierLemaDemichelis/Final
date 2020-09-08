using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitCardsStock : MonoBehaviour
{
    [SerializeField]
    CardInfo[] cardsTypes = null;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++) 
        {
            AddCard(cardsTypes[0]);
        }
        for (int i = 0; i < 5; i++)
        {
            AddCard(cardsTypes[1]);
        }
        for (int i = 0; i < 2; i++)
        {
            AddCard(cardsTypes[2]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddCard(CardInfo _card)
    {
        GameObject inventory = GameObject.FindGameObjectWithTag("DeckInformation");
        if (inventory != null)
        {
            inventory.GetComponent<DeckInformation>().AddCardToInventory(_card);
        }
    }
}
