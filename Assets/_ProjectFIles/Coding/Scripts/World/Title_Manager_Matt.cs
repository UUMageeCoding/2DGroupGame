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
        canvas = GetComponent<Canvas>();
        canvas.enabled = true;
        activeStart = true; 
    }

    void Update()
    {
        if(Input.anyKey && activeStart == true)
        {
            Debug.Log("Away Start");
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
}
