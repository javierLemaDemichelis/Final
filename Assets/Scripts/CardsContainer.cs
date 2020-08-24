using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CardsContainer : MonoBehaviour
{
    [SerializeField]
    TextMeshPro quantityOfCard = null;
    [SerializeField]
    GameObject card = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCardInfo(CardStock _cardStock) 
    {
        quantityOfCard.text = "x "+_cardStock.GetCount().ToString();
        CardInfo cardInfo = _cardStock.GetCardInfo();
        //Debug.Log(cardInfo.description);
        card.GetComponent<Card>().SetCardInfo(cardInfo);
    }
}
