using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject transitionManager;

    // Start is called before the first frame update
    private void Awake()
    {
        transitionManager = GameObject.FindGameObjectWithTag("TransitionManager");
    }
    void Start()
    {
        
        if (transitionManager != null)
        {
            
            transitionManager.GetComponent<TransitionManager>().StartFadeIn();
            
        }
        else
        {
            try
            {
                transitionManager = GameObject.FindGameObjectWithTag("TransitionManager");
            }
            catch (Exception e) 
            {
                Debug.Log(e);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play() 
    {
        InitTransition(1);
    }
    public void MenuScene() 
    {
        if (transitionManager != null)
        {
            //Debug.Log("play at first atempt");
            transitionManager.GetComponent<TransitionManager>().SetSceneIndexToChange(0);
            transitionManager.GetComponent<TransitionManager>().StartFadeOut();
        }
        else
        {
            //Debug.Log("play after first atempt");
            transitionManager = GameObject.FindGameObjectWithTag("TransitionManager");
            Play();
        }
    } 
    public void Deck() 
    {
        InitTransition(2);
    }
    public void Store() 
    {
        InitTransition(3);
    }
    public void Credits() 
    {
    
    }
    public void Exit() 
    {
        Application.Quit();
    }
    public void InitTransition(int index) 
    {
        if (transitionManager != null)
        {
            //Debug.Log("play at first atempt");
            transitionManager.GetComponent<TransitionManager>().SetSceneIndexToChange(index);
            transitionManager.GetComponent<TransitionManager>().StartFadeOut();
        }
        else
        {
            //Debug.Log("play after first atempt");
            try
            {
                transitionManager = GameObject.FindGameObjectWithTag("TransitionManager");
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

        }
    }
}
