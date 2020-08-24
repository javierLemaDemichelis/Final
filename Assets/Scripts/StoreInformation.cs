using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreInformation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    CardInfo normalPunch = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BItem0()
    {
        GameObject inventory = GameObject.FindGameObjectWithTag("DeckInformation");
        if (inventory != null) 
        {
            Debug.Log("carta comprada");
            inventory.GetComponent<DeckInformation>().AddCardToInventory(normalPunch);
        }
    }
}
