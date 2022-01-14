using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float topSpeed;
    public float jumpForce;
    public float upwardGravity;
    public float downwardGravity;
    private float moveInput;
    private float speed;
    public float acceleration;
    public float deceleration;
    public float turnAroundPenalty;
    public float airPenalty;
    public float turnAroundAirPenalty;

    private Rigidbody2D rb;
    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public Animator animator;

    public int extraJumps;
    public float dashDistance;
    public float dashTime; 
    private bool isDashing;
    public float dashCooldown;
    private float nextDashTime;


    private void Start()
    {
        moveInput = 0;
        rb = GetComponent<Rigidbody2D>();
        speed = 0;
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        updateAnimator();

        if(isDashing == false)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            if (moveInput != 0 && (facingRight && moveInput < 0) || (!facingRight && moveInput > 0))
            {
                speed *= -1 * turnAroundPenalty;
                //Debug.Log("speed");
                if(isGrounded == false)
                {
                    speed *= -1 * turnAroundAirPenalty;  
                }
            }

            if (moveInput == 0) //decel
            {
                if (speed > 0)
                {
                    if (isGrounded == false)
                    {
                        speed = speed-deceleration * airPenalty;
                    }
                    else
                    {
                        speed = (speed - deceleration);

                        if (speed < 0)
                        {
                            speed = 0;
                        }
                    }
                }
            }
            else if (speed < topSpeed) //accel
            {
                if (isGrounded == false)
                {
                    speed = speed + acceleration* airPenalty;
                }
                else
                {
                    speed = speed + acceleration;
                }
            }

            
            if (speed != 0)
            {
                if(facingRight)
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
                
            }
            else
            {
                rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            }


            if (facingRight == false && moveInput > 0)
            {
                flip();
            }
            else if (facingRight == true && moveInput < 0)
            {
                flip();
            }
            
        }
    }
    private void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = 1;
        }
        else
        {
            if (rb.velocity.y >= 0)
            {
                rb.gravityScale = upwardGravity;
            }
            else
            {
                rb.gravityScale = downwardGravity;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            if(Time.time > nextDashTime)
            {
                if (facingRight == true)
                {
                    nextDashTime = Time.time + dashCooldown; 
                    StartCoroutine(Dash(1));
                }
                if (facingRight == false)
                {
                    nextDashTime = Time.time + dashCooldown;
                    StartCoroutine(Dash(-1));
                }
            }
        }
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }
    void flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }
    IEnumerator Dash(int direction)
    {
        isDashing = true;
        rb.velocity = new Vector2(0f, 0f);
        rb.AddForce(new Vector2(direction * dashDistance, 0f), ForceMode2D.Impulse);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0f;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        rb.gravityScale = gravity;
    }
    
    private void updateAnimator()
    {
        animator.SetFloat("AnimationSpeed", Mathf.Abs(moveInput * speed));
        if (isGrounded == false)
        {
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }
        animator.SetFloat("VerticalVelocity", rb.velocity.y);
    }
}