using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ButtonOption : MonoBehaviour
{
    [SerializeField] GameObject father;
    [SerializeField] TextMeshProUGUI optionText;
    bool optionSelected = false;
    OptionForEvent optionInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitializeButtonInfo(GameObject _father, OptionForEvent _option) 
    {
        this.father = _father;
        optionInfo = _option;
        SetOptionText(_option.Title);
    }
    public void SetOptionText(string _text) 
    {
        this.optionText.text = _text;
    }
    public void SelectOption() 
    {
        optionSelected = true;
        ContactFather();
    }
    private void ContactFather() 
    {
        if (father != null) 
        {
            Debug.Log("objeto padre contactado");
            father.GetComponent<EventChat>().NotifySelectOption(this.optionInfo);
        }
    }
    public OptionForEvent GetOptionInfo() { return optionInfo; }
}
