using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    
    
    [SerializeField]
    public Enumerations.Interactuable interactuable = Enumerations.Interactuable.Card;
    [SerializeField]
    GameObject objectInContact=null;
    [SerializeField]
    int mode = 0;//mode 0 detect cards/mode 1 detect heroGroupCharacters /mode2 detect any npc
    public GameObject GetObjectInContact() 
    {
        return objectInContact;
    }
    public void Reset()
    {
        objectInContact = null;
    }
    public void SetInteractuable(Enumerations.Interactuable newInteractuable) 
    {
        this.interactuable = newInteractuable;
    }
    private void OnTriggerStay(Collider collision)
    {
        /*
        switch (interactuable) 
        {
            case Enumerations.Interactuable.Card:
                if (collision.CompareTag("Card")) 
                {
                    objectInContact = collision.gameObject;
                }
                break;
            case Enumerations.Interactuable.Npc:
                if (collision.CompareTag("Npc"))
                {
                    if (collision.gameObject.GetComponent<Npc>().GetItsPlayerGroup()) 
                    {
                        objectInContact = collision.gameObject;
                    }
                    
                }
                break;
        }*/
        switch (mode)
        {
            case 0:
                if (collision.CompareTag("Card"))
                {
                    objectInContact = collision.gameObject;
                }
                break;
            case 1:
                if (collision.CompareTag("Npc"))
                {
                    if (collision.gameObject.GetComponent<Npc>().GetItsPlayerGroup())
                    {
                        objectInContact = collision.gameObject;
                    }
                }
                break;
            case 2:
                if (collision.CompareTag("Npc"))
                {
                    objectInContact = collision.gameObject;
                }
                break;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        /*switch (interactuable)
        {
            case Enumerations.Interactuable.Card:
                if (collision.CompareTag("Card"))
                {
                    objectInContact = null;
                }
                break;
            
            case Enumerations.Interactuable.Npc:
                if (collision.CompareTag("Npc"))
                {
                    if (collision.gameObject.GetComponent<Npc>().GetItsPlayerGroup())
                    {
                        objectInContact = null;
                    }
                }
                break;
        }*/
        switch (mode) 
        {
            case 0:
                if (collision.CompareTag("Card"))
                {
                    objectInContact = null;
                }
                break;
            case 1:
                if (collision.CompareTag("Npc"))
                {
                    if (collision.gameObject.GetComponent<Npc>().GetItsPlayerGroup())
                    {
                        objectInContact = null;
                    }
                }
                break;
            case 2:
                if (collision.CompareTag("Npc"))
                {
                    objectInContact = null;
                }
                break;
        }
    }
    public void SetMode(int _mode) 
    {
        this.mode = _mode;
    }
}
