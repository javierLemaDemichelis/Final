using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pointer : MonoBehaviour
{
    [SerializeField]
    GameObject graphic = null;
    bool pointerActive = false;
    [SerializeField]
    GameObject cardGrabbed=null;
    [SerializeField]
    GameObject sensor = null;
    [SerializeField]
    Enumerations.GameState state = Enumerations.GameState.Adventure;
    [SerializeField]
    GameObject temp=null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    private void Update()
    {
        switch (state)
        {
            case Enumerations.GameState.Drag:
                Drag();
                break;
            case Enumerations.GameState.Action:
                Action();
                break;
            case Enumerations.GameState.Loot:
                break;
            case Enumerations.GameState.Endgame:
                break;
        }
        

    }
    private GameObject CheckForCard() 
    {
        GameObject cardToRetrieve = null ;

        return cardToRetrieve;
    }
    public void SetState(Enumerations.GameState newState)
    {
        this.state = newState;
        switch (state)
        {
            case Enumerations.GameState.Drag:

                sensor.SetActive(false);
                //sensor.GetComponent<Sensor>().SetInteractuable(Enumerations.Interactuable.Hero);
                //Drag();
                break;
            case Enumerations.GameState.Action:
                //sensor.GetComponent<Sensor>().SetInteractuable(Enumerations.Interactuable.Npc);
                //Action();
                break;
            case Enumerations.GameState.Loot:
                break;
            case Enumerations.GameState.Endgame:
                break;
        }

    }
    public void Drag() 
    {

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 position = new Vector3(worldPosition.x, worldPosition.y, graphic.transform.position.z);
        sensor.transform.position = new Vector3(graphic.transform.position.x, graphic.transform.position.y, sensor.transform.position.z);
        if (Input.GetMouseButtonDown(0))
        {
            //graphic.SetActive(true);
            pointerActive = true;
            
            graphic.transform.position = position;
            RaycastHit hit = new RaycastHit();
            
            if (Physics.Raycast(graphic.transform.position, graphic.transform.TransformDirection(Vector3.forward), out hit) != false)
            {
                //Debug.Log("nombre del objeto:" + hit.transform.tag);
                switch (hit.transform.tag)
                {
                    case "Card":
                        hit.transform.parent = graphic.transform;
                        cardGrabbed = hit.transform.gameObject;
                        break;
                }
            }
        }

        if (pointerActive)
        {
            graphic.transform.position = position;
            //Debug.DrawRay(graphic.transform.position, graphic.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            if (cardGrabbed != null)
            {
                cardGrabbed.transform.position = new Vector3(graphic.transform.position.x, graphic.transform.position.y, -5);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            pointerActive = false;
            if (cardGrabbed != null)
            {
                cardGrabbed.transform.position = new Vector3(graphic.transform.position.x, graphic.transform.position.y, 0);
                
                if (cardGrabbed.GetComponent<Card>().CheckForContactForAction())
                {
                    Debug.Log("action succes");
                    sensor.GetComponent<Sensor>().SetMode(2);

                }
                else 
                {
                    cardGrabbed.transform.parent = cardGrabbed.GetComponent<Card>().GetContainer();
                    cardGrabbed.transform.localPosition = Vector3.zero;
                    cardGrabbed = null;
                }
            }
        }
    }
    public void Action() 
    {

        temp = sensor.GetComponent<Sensor>().GetObjectInContact();
        //Vector3 worldPositionSensor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 positionSensor = new Vector3(worldPositionSensor.x, worldPositionSensor.y, sensor.transform.position.z);
        //sensor.transform.position = new Vector3(positionSensor.x, positionSensor.y, sensor.transform.position.z);
        if (Input.GetMouseButtonDown(0))
        {
            //sensor.SetActive(true);
            //graphic.SetActive(true);
            pointerActive = true;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 position = new Vector3(worldPosition.x, worldPosition.y, graphic.transform.position.z);
            graphic.transform.position = position;
            
        }

        if (pointerActive)
        {
            sensor.SetActive(true);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 position = new Vector3(worldPosition.x, worldPosition.y, graphic.transform.position.z);
            graphic.transform.position = position;
            //Debug.DrawRay(graphic.transform.position, graphic.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            
            sensor.transform.position = new Vector3(position.x, position.y, sensor.transform.position.z);
            //RaycastHit hit = new RaycastHit();
            graphic.transform.position = position;
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            sensor.transform.position=new Vector3(graphic.transform.position.x, graphic.transform.position.y, sensor.transform.position.z);
            if (temp != null) 
            {
                if (temp.transform.CompareTag("Npc"))
                {
                    if (!temp.GetComponent<Npc>().GetItsInanimateObject() && temp.GetComponent<Npc>().GetLifeValue() > 0) 
                    {
                        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
                        gameManager.GetComponent<GameManagerController>().SetObjectiveForAction(temp.transform.gameObject);
                        Debug.Log("objetivo fijado");
                        sensor.GetComponent<Sensor>().Reset();
                    }
                    else 
                    {
                        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
                        gameManager.GetComponent<GameManagerController>().ReturnCard();
                        gameManager.GetComponent<GameManagerController>().SetState(Enumerations.GameState.Drag);
                        Debug.Log("objetivo perdido");

                    }
                    
                    
                }
                else
                {
                    GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
                    gameManager.GetComponent<GameManagerController>().ReturnCard();
                    gameManager.GetComponent<GameManagerController>().SetState(Enumerations.GameState.Drag);
                    Debug.Log("objetivo perdido");


                }
            }
            else
            {
                GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
                gameManager.GetComponent<GameManagerController>().ReturnCard();
                gameManager.GetComponent<GameManagerController>().SetState(Enumerations.GameState.Drag);
                Debug.Log("objetivo perdido");


            }

            


            pointerActive = false;
            sensor.SetActive(false);
            
        }
    }
    public void SetModeOfSensor(int _mode) 
    {
        sensor.GetComponent<Sensor>().SetMode(_mode);
    }
}
