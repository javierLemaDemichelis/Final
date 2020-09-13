using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NpcUI : MonoBehaviour
{
    [SerializeField] TextMeshPro actionPoints;
    [SerializeField] TextMeshPro life;
    int healthPoints = 0;
    int healthPointsMax = 0;

    public void SetTextAc(string _text) 
    {
        actionPoints.text = "AC " + _text;
    }
    public void SetTextHealth(string _lifeMax,string _life)
    {
        life.text = _life+"/"+_lifeMax;
    }
    public void SetActionSpended(bool state) 
    {
        if (state) 
        {
            SetTextAc("0");
        }
        else 
        {
            SetTextAc("1");
        }
    }
    public void SetHealth(int maxHealth,int health) 
    {
        healthPointsMax = maxHealth;
        healthPoints = health;
        SetTextHealth(healthPointsMax.ToString(), healthPoints.ToString());
    }
    public void UpdateHealth(int health)
    {
        healthPoints = health;
        SetTextHealth(healthPointsMax.ToString(), healthPoints.ToString());
    }
    public void HideAc(bool state) 
    {
        if (state)
        {
            actionPoints.gameObject.SetActive(false);
        }
        else 
        {
            actionPoints.gameObject.SetActive(true);
        }
    }
}
