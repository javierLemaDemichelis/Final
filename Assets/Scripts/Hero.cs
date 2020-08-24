using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    //[SerializeField]
    //HeroInfo heroInfo=null;
    [SerializeField]
    GameObject graphic=null;
    [SerializeField]
    GameObject outline=null;
    [SerializeField]
    GameObject spawnPoint = null;
    [SerializeField]
    GameObject explotionEffect;
    bool outlineShowed = false;
    bool characterAlive = true;
    [SerializeField]
    bool actionExpended = false;
    int life=0;
    int attack=0;
    int defence=0;
    int heal=0;

    // Start is called before the first frame update
    void Start()
    {
        //SetHeroInfo(heroInfo);
    }

    // Update is called once per frame
    void Update()
    {
        if (outlineShowed)
        {
            outline.SetActive(true);
            
        }
        else 
        {
            outline.SetActive(false);
        }
    }/*
    public void SetHeroInfo(HeroInfo _heroInfo) 
    {
        heroInfo = _heroInfo;
        graphic.GetComponent<SpriteRenderer>().sprite = heroInfo.graphic;
        life = heroInfo.LifeValue;
        attack = heroInfo.OffensiveValue;
        defence = heroInfo.DefensiveValue;
        heal = heroInfo.HealingValue;

    }*/
    /*public HeroInfo GetHeroInfo()
    {
        return heroInfo;
    }*/
    public void ShowOutline() 
    {
        outlineShowed = true;
        
    }
    public void HideOutline()
    {
        outlineShowed = false;

    }
    public GameObject GetSpawnPoint() 
    {
        return spawnPoint;
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform.CompareTag("Sensor"))
        {
            ShowOutline();
        }
        
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.CompareTag("Sensor"))
        {
            HideOutline();
        }
        
    }
    public void SetLifeValue(int value) 
    { 
        this.life = value;
        if (life <= 0)
        {
            life = 0;
            characterAlive = false;
        }
    }
    public void RecieveAttack()
    {
        explotionEffect.GetComponent<Explotion>().PlayEffect();
    }
    public void SetAttackValue(int value) { this.attack = value; }
    public void SetDefenseValue(int value) { this.defence = value; }
    public void SetHealValue(int value) { this.heal = value; }
    public void SetActionExpended(bool value) { this.actionExpended = value; }
    public int GetLifeValue() { return this.life; }
    public int GetAttackValue() { return this.attack; }
    public int GetDefenseValue() { return this.defence; }
    public int GetHealValue() { return this.heal; }
    public bool GetActionExpended() { return this.actionExpended; }
    public bool GetcharacterState() { return this.characterAlive; }
}
