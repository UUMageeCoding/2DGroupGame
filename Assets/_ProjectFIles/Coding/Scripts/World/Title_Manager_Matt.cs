using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Manager_Matt : MonoBehaviour
{
    #region Variables

    private Canvas canvas;

    private bool activeStart;

    #endregion
    void Start()
    {
        Debug.Log("Start Timer");
        StartCoroutine(LoadTime(3,1));
    }

    void Update()
    {
        if(Input.anyKey && activeStart == true)
        {
            //Debug.Log("Away Start");
            activeStart = false;
        }
    }
    private void FixedUpdate()
    {
        if(activeStart == false)
        {
            canvas.enabled = false;
        }
    }
    private void ActivateScreen()
    {
        activeStart = true;
        canvas = GetComponent<Canvas>();
        canvas.enabled = true;
    }
    private IEnumerator LoadTime (int totalTime, int idTime)
    {
        yield return new WaitForSeconds(totalTime);
        Debug.Log("End Timer");

        if(idTime == 1)
        {
            ActivateScreen();
        }
    }
}
