using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public float airResistance;
    public float turnAroundAirPenalty;
    public float instantBoost;

    private Rigidbody2D rb;
    private bool facingRight = true;

    private bool isGrounded;
    private bool inDoor;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsDoor;

    public Animator animator;

    public int extraJumps;
    public float dashDistance;
    public float dashTime; 
    private bool isDashing;
    public float dashCooldown;
    private float nextDashTime;

    private bool isWallSliding;
    public Transform wallCheckRight;
    public Transform wallCheckLeft;

    private bool pauseMovement;
    private int pauseMovementCounter;

    private bool isAlive;

    //public GameObject door;


    private void Start()
    {
        moveInput = 0;
        rb = GetComponent<Rigidbody2D>();
        speed = 0;
        pauseMovement = false;
        pauseMovementCounter = 5;   
        isAlive = true;
    }
    private void FixedUpdate()
    {
        inDoor = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsDoor);
        if (inDoor)
        {
            print("indoor");
            SceneManager.LoadScene("2");
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        isWallSliding = Physics2D.OverlapCircle(wallCheckLeft.position, checkRadius, whatIsGround); //removed wallcheck right

        if (isWallSliding)
        {
            if (!isGrounded)
            {
                //we dont know why this is here, but I swear if u remove it everything breaks
            }
        }
        updateAnimator();

        if(speed > topSpeed)
        {
            speed = topSpeed;
        }

        if(isDashing == false && isAlive)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            if(pauseMovement)
            {
                moveInput = 0;
                pauseMovementCounter -= 1;
                if(pauseMovementCounter <= 0)
                {
                    pauseMovement = false;
                    pauseMovementCounter = 5;
                }
            }
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

                    }

                    if (speed < 0)
                    {
                        speed = 0;
                    }
                }
            }
            else if (speed < topSpeed) //accel
            {
                if (isGrounded == false)
                {
                    if(speed < topSpeed * airResistance)
                    speed = speed+ instantBoost + acceleration* airPenalty;
                }
                else
                {
                    speed = speed+instantBoost + acceleration;
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
            rb.gravityScale = downwardGravity;
        }
        else
        {
            if (rb.velocity.y >= 0)
            {
                rb.gravityScale = upwardGravity;
            }
            else
            {
                if (!isWallSliding)
                {
                    rb.gravityScale = downwardGravity;
                }
                else
                {
                    rb.gravityScale = 0;
                    rb.velocity = new Vector2(rb.velocity.x, -1);
                }
            }
        }
        if (isAlive)
        {
            if (Input.GetKeyDown(KeyCode.K) && isAlive)
            {
                if (Time.time > nextDashTime)
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

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (inDoor)
                {
                    print("u moved on");
                }
            }
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
            if (isWallSliding)
            {
                animator.SetBool("IsJumping", true); //if adding wall sliding back, set this to false
                animator.SetBool("IsWallSliding", false);//we removed the wall sliding animation by setting to false
            } else
            {
                animator.SetBool("IsWallSliding", false);
                animator.SetBool("IsJumping", true);
            }
        }
        else
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsWallSliding", false);
        }
        animator.SetFloat("VerticalVelocity", rb.velocity.y);
    }

    public void gotHit()
    {
        rb.velocity = new Vector2(0f, 0f);
        speed = 0;
        pauseMovement = true;
        pauseMovementCounter = 5;
    }

    public void setAlive(bool aliveStatus)
    {
        isAlive = aliveStatus;
    }
}