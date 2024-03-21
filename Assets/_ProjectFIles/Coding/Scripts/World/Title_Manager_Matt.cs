using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Manager_Matt : MonoBehaviour
{
    #region Variables

    private Canvas canvas;
    public CanvasGroup canvasGroup;

    private bool activeStart;

    private int alphaValue;

    #endregion
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        //Debug.Log("Start Timer");
        canvasGroup.alpha = 0f;
        StartCoroutine(LoadTime(3f, 1));
    }

    void Update()
    {
        if(Input.anyKey && activeStart == true)
        {
            //Debug.Log("Away Start");
            FadeOut();
            activeStart = false;
            canvas.enabled = false;
        }
    }
    private void ActivateScreen()
    {

        activeStart = true;
        canvas.enabled = true;
        while (alphaValue != 1)
        {
            StartCoroutine(LoadTime(0.2f, 0));
            canvasGroup.alpha += 0.1f;
            canvasGroup.alpha = alphaValue;
        }

    }
    private void FadeIn()
    {
        
    }
    private void FadeOut()
    { 
        
    }

    private IEnumerator LoadTime (float totalTime, int idTime)
    {
        yield return new WaitForSeconds(totalTime);
        //Debug.Log("End Timer");

        if(idTime == 1)
        {
            ActivateScreen();
        }
    }
}
