using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

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
    private bool isJumpPressed = false;
    [SerializeField] private GameObject chute;

    public bool canMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 1;
        animator = GetComponent<Animator>();
        chute.SetActive(false);
        ChangeState(playerSpawn);
        canMove = false;    
    }
    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            isJumpPressed = true;
        }
        //Press Any Button to Spawn----------------------------------------
        if (currentState == "PlayerSpawn" && Input.anyKey)
        {
            canMove = true;
            ChangeState(playerIdle);
        }
        //-----------------------------------------------------------------
    }

    private void FixedUpdate()
    {
        
        if (canMove)
        {
            //Moving-------------------------------------------------------
            rb.velocity = new Vector2(xAxis * playerSpeed, rb.velocity.y);
            Debug.Log("Player has moved");

            if (IsGrounded())
            {
                rb.drag = 1;
                chute.SetActive(false);

                if (xAxis != 0)
                {
                    ChangeState(playerRun);
                }
                else
                {
                    ChangeState(playerIdle);
                }
            }
            //-------------------------------------------------------------

            //Jumping------------------------------------------------------
            if (isJumpPressed && IsGrounded())
            {
                rb.AddForce(new Vector2(0, jumpingPower));
                isJumpPressed = false;
                ChangeState(playerJump);
                Debug.Log("Player has Jumped");
            }
            //Chute--------------------------------------------------------
            if (!isJumpPressed && !IsGrounded())
            {
                chute.SetActive(true);
                rb.drag = 5;
                Debug.Log("Player has Chuted");
            }

            //-------------------------------------------------------------

        }

        /*


        if (Input.GetButtonDown("Jump") && IsGrounded() && countJump == false)
        {
            Debug.Log("Jump");
            countJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            //animator.SetBool("IsJump", true);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded() == false && countJump == true)
        {
            Debug.Log("Para");
            chute.SetActive(true);
            rb.drag = 5;
            countJump = false;
            //Debug.Log(countJump);
        }

        if (rb.velocity.y < 0f)
        {
            Debug.Log("Falling");
            countJump = true;
        }

        if (IsGrounded() && countJump == true) 
        {
            Debug.Log("Reset Jump");
            countJump = false;
        }

        if (!Input.GetButton("Jump") && IsGrounded())
        {
            //Debug.Log("Landed");
            animator.SetBool("IsJump", false);
        }

        if (Input.GetButton("Horizontal") && IsGrounded())
        {
            //Debug.Log("Moving");
            animator.SetBool("IsWalk", true);
        }

        if (Input.GetButtonDown("Horizontal") && !IsGrounded())
        {
            //Debug.Log("Flying");
            animator.SetBool("IsWalk", false);
        }

        if (Input.GetButtonUp("Horizontal") && IsGrounded())
        {
            //Debug.Log("Stop Moving");
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsJump", false);
        }
        */
        Flip();
        
    }

    private bool IsGrounded()
    {
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
            //animator.Play(newState);
            //currentState = newState;
        }
    }

    public void LockMove ()
    {
        canMove = false;
    }
    public void UnlockMove()
    {
        canMove = true;
    }
}
