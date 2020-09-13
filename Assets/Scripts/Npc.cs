using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField]
    NpcInfo npcInfo;
    [SerializeField]
    GameObject graphic = null;
    [SerializeField]
    GameObject outline = null;
    [SerializeField]
    GameObject spawnPoint = null;
    [SerializeField] GameObject npcUI;
    [SerializeField]
    GameObject explotionEffect;
    bool outlineShowed = false;
    bool characterAlive = true;
    [SerializeField]
    bool actionExpended = false;
    int life = 0;
    int attack = 0;
    int defence = 0;
    int heal = 0;
    bool itsInanimateObject=false;
    bool itsLooteable=false;
    [SerializeField]
    bool itsPlayerGroup = false;
    LootInfo loot;
    void Start()
    {
        //SetNpcInfo(npcInfo);
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
    }
    public void SetNpcInfo(NpcInfo _npcInfo)
    {
        npcInfo = _npcInfo;
        graphic.GetComponent<SpriteRenderer>().sprite = npcInfo.graphic;
        life = npcInfo.LifeValue;
        attack = npcInfo.OffensiveValue;
        defence = npcInfo.DefensiveValue;
        heal = npcInfo.HealingValue;
        npcUI.GetComponent<NpcUI>().SetHealth(_npcInfo.LifeValue, life);
        npcUI.GetComponent<NpcUI>().SetActionSpended(actionExpended);
        itsLooteable = _npcInfo.itsLooteable;
        itsInanimateObject = _npcInfo.inanimateObject;
        loot = _npcInfo.loot;
    }
    public NpcInfo GetNpcInfo()
    {
        return npcInfo;
    }
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
        else 
        {
            HideOutline();
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
            this.graphic.SetActive(false);
        }
        npcUI.GetComponent<NpcUI>().UpdateHealth(life);
    }
    public void RecieveAttack() 
    {
        explotionEffect.GetComponent<Explotion>().PlayEffect();
    }
    public void SetAttackValue(int value) { this.attack = value; }
    public void SetDefenseValue(int value) { this.defence = value; }
    public void SetHealValue(int value) { this.heal = value; }
    public void SetActionExpended(bool value) 
    { 
        this.actionExpended = value;
        npcUI.GetComponent<NpcUI>().SetActionSpended(value);
    }
    public int GetLifeValue() { return this.life; }
    public int GetAttackValue() { return this.attack; }
    public int GetDefenseValue() { return this.defence; }
    public int GetHealValue() { return this.heal; }
    public bool GetActionExpended() { return this.actionExpended; }
    public bool GetcharacterState() { return this.characterAlive; }

    public void setItsPlayerGroup(bool state) 
    {
        itsPlayerGroup = state;
    }
    public bool GetItsPlayerGroup()
    {
        return itsPlayerGroup;
    }
    public bool GetItsLooteable() 
    {
        return itsLooteable;
    }
    public bool GetItsInanimateObject()
    {
        return itsInanimateObject;
    }
    public LootInfo GetLoot() 
    {
        return loot;
    }
}
