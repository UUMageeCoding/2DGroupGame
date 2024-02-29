using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player_Behaviour : MonoBehaviour
{
    private float xAxis;
    private float yAxis;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float jumpingPower = 16f;
    private bool isFacingRight = true;

    //Animation-----------------------------------------------------------
    private Animator animator;
    private string currentState;
    const string playerSpawn = "PlayerSpawn";
    const string playerIdle = "PlayerIdle";
    const string playerRun = "PlayerRun"; 
    const string playerJump = "PlayerJump";
    const string playerClimb = "PlayerClimb";
    const string playerFall = "PlayerFall";
    const string playerDeath = "PlayerDeath";
    const string playerChute = "PlayerChute";
    const string playerStick = "PlayerStick";
    //--------------------------------------------------------------------

    private Rigidbody2D rb;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private bool countJump;
    private bool chuteUsed;

    [SerializeField] private GameObject chute;

    public bool canMove;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 1;

        animator = GetComponent<Animator>();

        chute.SetActive(false);
        chuteUsed = false;

        ChangeState(playerSpawn);
        LockMove();  
    }
    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

        //Press Any Button to Spawn----------------------------------------
        if (currentState == "PlayerSpawn" && Input.anyKey)
        {
            //Debug.Log("Player has spawned");
            UnlockMove();
            ChangeState(playerIdle);
        }
        //-----------------------------------------------------------------
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(xAxis * playerSpeed, rb.velocity.y);

            if (Input.GetButtonDown("Jump"))
            {
                if (IsGrounded())
                {
                    Debug.Log("Jump");
                    ChangeState(playerJump);
                    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                }
            }
            /*
            if (Input.GetButtonDown("Jump") && IsGrounded() == false)
            {
                Debug.Log("Chute");
                ChangeState(playerChute);
                chute.SetActive(true);
                rb.drag = 5;
                chuteUsed = true;
                //Debug.Log(countJump);
            }
            */
            if (rb.velocity.y < 0f)
            {
                //Debug.Log("Falling");
                countJump = true;
            }

            if (IsGrounded() && chuteUsed == true)
            {
                //Debug.Log("Reset Jump");
                countJump = false;
            }

            if (Input.GetButton("Horizontal"))
            {
                //Debug.Log("Moving");
                ChangeState(playerRun);
            }

            
            Flip();
        }
    }

    private bool IsGrounded()
    {
        Debug.Log("Ground");
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && xAxis < 0f || !isFacingRight && xAxis > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground") 
        {
            chute.SetActive(false);
            Debug.Log("Ground");
            rb.drag = 1;
            countJump = false;
            //Debug.Log(countJump);
        }
    }
    */

    void ChangeState(string newState)
    {
        if (currentState == newState) return;
        {
            //Debug.Log("Current State - " + currentState);
            //animator.Play(newState);
            currentState = newState;
        }
    }

    public void LockMove ()
    {
        canMove = false;
        //Debug.Log("Lock Move");
    }
    public void UnlockMove()
    {
        canMove = true;
        //Debug.Log("Unlock Move");
    }
}
