using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Manager_Matt : MonoBehaviour
{
    private Canvas canvas;
    public Canvas titleCanvas;

    public CanvasGroup group;

    public GameObject player;

    [SerializeField] private bool fadeIn = false;

    [SerializeField] private bool fadeOut = false;

    private bool activteScreen;

    private float alphaMultiplier;

    public float spawnTime;

    void Start()
    {
        player = GameObject.Find("Player_Corto");
        canvas = GetComponent<Canvas>();
        canvas.enabled = true;
        group.alpha = 0f;
        fadeIn = true;
    }

    void Update()
    {
        if (titleCanvas.enabled && Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Pause");
            if (fadeIn)
            {
                FadeIn();
            }
            if (fadeOut)
            {
                FadeOut();
            }
        }
    }
    private void FadeIn()
    {
        Debug.Log("Start Fade In");

        while (group.alpha < 1)
        {
            group.alpha += Time.deltaTime;
            //Debug.Log("Fading In");


            if (group.alpha >= 1)
            {
                //Debug.Log("Fading Done");
                canvas.enabled = true;
                fadeIn = false;
                fadeOut = true;
                activteScreen = true;

            }
        }

    }
    private void FadeOut()
    {
        activteScreen = false;
        fadeOut = true;
        //Debug.Log("Start Fade Out");
        if (group.alpha >= 0)
        {
            group.alpha -= Time.deltaTime * alphaMultiplier;

            if (group.alpha <= 0)
            {
                canvas.enabled = false;
                fadeOut = false;
                fadeIn = true;
                StartCoroutine(ActiavetePlayer());
            }
        }
    }

    IEnumerator ActiavetePlayer()
    {
        yield return new WaitForSeconds(spawnTime);
        //Debug.Log("Wait Done");
        player.GetComponent<Player_Behaviour>().unlockMovement();
    }
}
