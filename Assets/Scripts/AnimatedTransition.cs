using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedTransition : MonoBehaviour
{
    [SerializeField]
    Image cover = null;
    [SerializeField]
    private bool fadeOutComplete = false;
    [SerializeField]
    private bool fadeInComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator FadeOut() 
    {
        yield return new WaitForSeconds(0.1f);
        if (!fadeOutComplete) 
        {
            //Debug.Log("start fadeout");
            cover.enabled = true;
            if (cover.color.a <1)
            {
                cover.color = new Color(cover.color.r, cover.color.g, cover.color.b, cover.color.a + 0.1f);
                StartCoroutine("FadeOut");
            }
            else 
            {
                fadeOutComplete = true;
                GameObject transitionManager = GameObject.FindGameObjectWithTag("TransitionManager");
                if (transitionManager != null) 
                {
                    transitionManager.GetComponent<TransitionManager>().SetFadeOutFinalized(true);
                }
            }
            
        }
    }
    IEnumerator FadeIn() 
    {
        yield return new WaitForSeconds(0.1f);
        if (!fadeInComplete)
        {
            
            if (cover.color.a >0)
            {
                cover.color = new Color(cover.color.r, cover.color.g, cover.color.b, cover.color.a - 0.1f);
                StartCoroutine("FadeIn");
            }
            else
            {
                fadeInComplete = true;
                cover.enabled = false;
                //Debug.Log("fade in complete");
                StopCoroutine("FadeIn");
            }

        }
    }
    public bool GetFadeInState() 
    {
        return fadeInComplete;
    }
    public bool GetFadeOutState()
    {
        return fadeOutComplete;
    }
}
