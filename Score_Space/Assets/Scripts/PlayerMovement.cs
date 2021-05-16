using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;
    public float momentum;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool isCooldown; 

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public Animator animator; 

    public int extraJumps;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        updateAnimator();
        
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        animator.SetFloat("AnimationSpeed", Mathf.Abs(moveInput * speed));

        if (facingRight == false && moveInput > 0)
        {
            flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            flip();
        }
    }
    private void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = 1;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (isCooldown == false)
            {
                animator.SetTrigger("Attack1");
                Cooldown();
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
    IEnumerator Cooldown()
    {
        isCooldown = true; 
        yield return new WaitForSeconds(.25f);
        isCooldown = false; 
    }
    private void updateAnimator()
    {
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