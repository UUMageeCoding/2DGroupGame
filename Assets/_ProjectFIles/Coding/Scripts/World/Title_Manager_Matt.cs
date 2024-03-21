using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Manager_Matt : MonoBehaviour
{
    #region Variables

    private Canvas canvas;
    public CanvasGroup group;

    [SerializeField] private bool fadeIn = false;

    [SerializeField] private bool fadeOut = false;

    private bool activteScreen;

    #endregion
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = true;
        group.alpha = 0f;
        fadeIn = true;
        FadeIn();
    }

    void Update()
    {
        if (fadeIn)
        {
            FadeIn();
        }
        if(Input.anyKey && activteScreen)
        {
            FadeOut();
        }
        if(fadeOut)
        { 
            FadeOut(); 
        } 

    }
    private void FadeIn()
    {
        //Debug.Log("Start Fade In");

        if (group.alpha < 1)
        {
               group.alpha += Time.deltaTime;
            //Debug.Log("Fading In");


            if (group.alpha >= 1) 
            {
                //Debug.Log("Fading Done");
                canvas.enabled = true;
                fadeIn = false; 
                activteScreen = true;
                
            }
        }        
        
    }
    private void FadeOut()
    {
        activteScreen = false;
        fadeOut = true;
        Debug.Log("Start Fade Out");
        if (group.alpha >= 0)
        {
            group.alpha -= Time.deltaTime;

            if (group.alpha == 0)
            {
                canvas.enabled = false;
                fadeOut = false;
            }
        }
    }
}
