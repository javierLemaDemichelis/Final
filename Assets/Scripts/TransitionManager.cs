using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private bool fadeOutFinalized = false;
    [SerializeField]
    private bool fadeInFinalized = false;
    [SerializeField]
    private bool fadeOutInit = false;
    [SerializeField]
    private bool fadeInInit = false;
    [SerializeField]
    GameObject animatedTransition = null;
    [SerializeField]
    string[] scenes = null;
    [SerializeField]
    int sceneIndexToChange=0;

    public static TransitionManager instance;

    void Start()
    {
        
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else 
        {
            if (instance!=this) 
            {
                Destroy(this.gameObject);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (animatedTransition != null)
        {
            if (fadeOutInit)
            {
                if (animatedTransition.GetComponent<AnimatedTransition>().GetFadeOutState())
                {
                    fadeOutFinalized = true;
                }
                if (fadeOutFinalized)
                {
                    //Debug.Log("change scene");
                    //Debug.Log("Scene:" + scenes[sceneIndexToChange].name);
                    SceneManager.LoadScene(scenes[sceneIndexToChange]);
                    fadeOutFinalized = false;
                    fadeOutInit = false;
                }
            }
            if (fadeInInit)
            {
                if (animatedTransition.GetComponent<AnimatedTransition>().GetFadeInState())
                {
                    fadeInFinalized = true;
                }
            }
        }
        else 
        {
            animatedTransition = GameObject.FindGameObjectWithTag("AnimatedTransition");
        }
        
    }
    public void StartFadeOut() 
    {
        
        fadeOutInit = true;
        GameObject animatedTransition = GameObject.FindGameObjectWithTag("AnimatedTransition");
        if (animatedTransition != null) 
        {
            
            animatedTransition.GetComponent<AnimatedTransition>().StartCoroutine("FadeOut");
        }
    }
    public void StartFadeIn()
    {
        fadeInInit = true;
        GameObject animatedTransition = GameObject.FindGameObjectWithTag("AnimatedTransition");
        
        if (animatedTransition != null)
        {
            animatedTransition.GetComponent<AnimatedTransition>().StartCoroutine("FadeIn");
            
        }
    }
    public void SetFadeOutFinalized(bool state) 
    {
        fadeOutFinalized = state;
    }
    public void SetSceneIndexToChange(int newIndex) 
    {
        sceneIndexToChange = newIndex;
    }
    public bool GetFadeOutState() 
    {
        return fadeInFinalized;
    }
}
