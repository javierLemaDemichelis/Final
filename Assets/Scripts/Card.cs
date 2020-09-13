using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Card : MonoBehaviour
{
    public Enumerations.CardState cardState = Enumerations.CardState.Drag;
    public Card(CardInfo _cardInfo) 
    {
        SetCardInfo(_cardInfo);
    }
    [SerializeField]
    CardInfo cardInfo = null;
    [SerializeField]
    TextMeshPro value = null;
    [SerializeField]
    TextMeshPro description = null;
    [SerializeField]
    GameObject graphic = null;
    [SerializeField]
    Transform container = null;
    [SerializeField]
    GameObject sensor = null;
    [SerializeField]
    GameObject objectOfSensor = null;
    
    private void Awake()
    {
        if (cardInfo != null) 
        {
            value.text = cardInfo.value.ToString();
            description.text = cardInfo.description.ToString();
            graphic.GetComponent<SpriteRenderer>().sprite = cardInfo.graphic;
        }
    }

    void Update()
    {
        if (cardState.ToString().Equals(Enumerations.CardState.Drag.ToString()))
        {
            
            CheckForContact();
        }
        
    }
    public void CheckForContact() 
    {
        objectOfSensor= sensor.GetComponent<Sensor>().GetObjectInContact();
    }
    public bool CheckForContactForAction()
    {
        bool contact = false;
        if (objectOfSensor != null) 
        {
            if (objectOfSensor.transform.CompareTag("Npc")) 
            {
                if (objectOfSensor.GetComponent<Npc>().GetLifeValue() > 0) 
                {
                    GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
                    gameManager.GetComponent<GameManagerController>().SetCardForAction(this.gameObject);
                    gameManager.GetComponent<GameManagerController>().SetHeroForAction(objectOfSensor);
                    gameManager.GetComponent<GameManagerController>().SetState(Enumerations.GameState.Action);
                    this.transform.position = objectOfSensor.GetComponent<Npc>().GetSpawnPoint().transform.position;
                    this.transform.parent = objectOfSensor.GetComponent<Npc>().GetSpawnPoint().transform;
                    contact = true;
                }
                
            }
        }
        return contact;
    }
    public void SetCardInfo(CardInfo _cardInfo) 
    {
        this.cardInfo = _cardInfo;
        if (cardInfo != null)
        {
            value.text = cardInfo.value.ToString();
            description.text = cardInfo.description.ToString();
            graphic.GetComponent<SpriteRenderer>().sprite = cardInfo.graphic;
        }
    }
    public CardInfo GetCardInfo() 
    {
        return this.cardInfo;
    }
    public void SetCardState(Enumerations.CardState newCardState) 
    {
        this.cardState = newCardState;
    }
    public void SetContainer(Transform _container) 
    {
        this.container = _container;
    }
    public Transform GetContainer()
    {
        return this.container;
    }
    public void ReturnToContainer() 
    {
        this.transform.parent = container.transform;
        this.transform.position = container.transform.position;
    }
}
