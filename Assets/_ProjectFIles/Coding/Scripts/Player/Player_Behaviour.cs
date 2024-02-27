using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class Player_Behaviour : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float playerSpeed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    private bool isFacingRight = true;

    private Animator animator;
    private Rigidbody2D rb;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private bool countJump;
    [SerializeField] private GameObject chute;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 1;
        playerSpeed = 5;
        animator = GetComponent<Animator>();
        chute.SetActive(false);
    }
    void Update()
    {
            horizontal = Input.GetAxisRaw("Horizontal");

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
        /*
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
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * playerSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        //Debug.Log("On Ground");
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

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
}
