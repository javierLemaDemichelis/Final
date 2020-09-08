using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
public class EventChat : MonoBehaviour
{
    [SerializeField] Transform panel;
    [SerializeField] GameObject optionButton;
    [SerializeField] TextMeshProUGUI eventText;
    [SerializeField] float separation=3.0f;
    [SerializeField] float positionInY = -1.3f;
    [SerializeField] float lastPositionX = 0.0f;
    [SerializeField] EventContainer eventContainer;
    List<GameObject> listOfOptions=new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitializeEventInfo(AdventureEvents adventureEventToInitialize, EventContainer _eventContainer) 
    {
        eventContainer = _eventContainer;
        eventText.text = adventureEventToInitialize.eventText0;
        for (int i = 0; i < adventureEventToInitialize.options.Length; i++)
        {
            CreateOption(adventureEventToInitialize.options[i],new Vector3(lastPositionX,positionInY,0));
            lastPositionX += separation;
        }
        
        //acomodo las posiciones de las opciones
        float distance = lastPositionX/adventureEventToInitialize.options.Length;
        for (int i = 0; i < listOfOptions.Count ; i++) 
        {
            listOfOptions[i].transform.localPosition = listOfOptions[i].transform.localPosition - new Vector3(distance,0,0);
        }

    }
    private void CreateOption(OptionForEvent optionForEvent,Vector3 position) 
    {
        GameObject option = Instantiate(optionButton, panel);

        option.GetComponent<ButtonOption>().InitializeButtonInfo(this.gameObject, optionForEvent);
        option.transform.localPosition = position;
        listOfOptions.Add(option);
    }
    public void HideEventChat(bool state) 
    {
        if (state) 
        {
            this.gameObject.SetActive(false);
        }
        else 
        {
            this.gameObject.SetActive(true);
        }
    }
    public void NotifySelectOption(OptionForEvent optionSelected) 
    {
        for (int i = 0; i < listOfOptions.Count; i++) 
        {
            if (optionSelected.Equals(listOfOptions[i].GetComponent<ButtonOption>().GetOptionInfo())) 
            {
                Debug.Log("opcion encontrada");
                eventContainer.DealWithOption(optionSelected);
            }
        }
    }
}
