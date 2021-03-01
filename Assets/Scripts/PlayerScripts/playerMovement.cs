using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float MovementSpeed = 5;
    private Rigidbody2D rb;
    public float JumpForce = 5;
    private Animator anim;
    public bool isGrounded;
    public Transform groundPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    public float dashForce;
    public float startDashTimer;
    public float currentDashTimer;
    public float dashDirection;
    public float moveX;
    public bool isDashing;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        isGrounded = Physics2D.OverlapCircle(groundPos.position, checkRadius, whatIsGround);

        anim.SetFloat("Speed", Mathf.Abs(movement));
        

        if(movement <= 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
            moveX = Input.GetAxis("Horizontal");
        }

        else
        {
            transform.eulerAngles = new Vector2(0, -180);
            moveX = -Input.GetAxis("Horizontal");
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            isGrounded = false;
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }

        if(isGrounded == true && Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetBool("isCrouching", true);
        }

        if(isGrounded == true && Input.GetKeyUp(KeyCode.LeftControl))
        {
            anim.SetBool("isCrouching", false);
        }

        if(isGrounded == true)
        {
            anim.SetBool("isJumping", false);
        }

        else
        {
            anim.SetBool("isJumping", true);
        }

        if(Input.GetKey(KeyCode.LeftShift) && moveX != 0)
        {
            isDashing = true;
            currentDashTimer = startDashTimer;
            rb.velocity = Vector2.zero;
            dashDirection = (int)moveX;
        }

        if(isDashing)
        {
            rb.velocity = transform.right * dashDirection * dashForce;
            currentDashTimer -= Time.deltaTime;

            if(currentDashTimer <= 0)
            {
                isDashing = false;
            }
        }
    }
}