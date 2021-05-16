using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool facingRight = true;
    public Rigidbody2D player;

    private float moveInput;
    private float speed;
    private float health;

    //private bool isGrounded;
    //public Transform groundCheck;
    //public float checkRadius;
    //public LayerMask whatIsGround;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        rb = GetComponent<Rigidbody2D>();
        speed = 5;

    }

    private void FixedUpdate()
    {

        if (facingRight)
        {
            moveInput = 1;
        }
        else 
        {
            moveInput = -1;
        }
        

        if ((player.position.x - rb.position.x) > .01)
        {
            if (facingRight == false)
            {
                flip();
            }
        }
        else if((player.position.x - rb.position.x) < -.01)
        {
            if (facingRight)
            {
                flip();
            }
        }
        if (Mathf.Abs(player.position.x - rb.position.x) < .8)
        {
            moveInput = 0;
            animator.SetTrigger("Attack1");
        }

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }


    void flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }
}