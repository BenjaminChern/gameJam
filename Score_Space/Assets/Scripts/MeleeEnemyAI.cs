using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool facingRight = true;
    public GameObject player;
    public float movementRange; 

    private float moveInput;
    private float speed;
    private float health;

    public float attackCooldown;
    private float nextAttackTime;

    public Transform hitbox;
    public float attackRange = .5f;
    public LayerMask playerLayer;


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

        
        if ((player.transform.position.x - rb.position.x) > .01)
        {
            if (facingRight == false)
            {
                flip();
            }
        }
        else if ((player.transform.position.x - rb.position.x) < -.01)
        {
            if (facingRight)
            {
                flip();
            }
        }
        
        if (Mathf.Abs(player.transform.position.x - rb.position.x) < attackRange)
        {
            moveInput = 0;
            if (Time.time > nextAttackTime)
            {
                animator.SetTrigger("Attack1");
                Collider2D hit = Physics2D.OverlapCircle(hitbox.position, attackRange, playerLayer);
                Debug.Log(hit.name + "test");
                nextAttackTime = Time.time + attackCooldown + ;
            }
        }
        if(Mathf.Abs(player.transform.position.x - transform.position.x) + Mathf.Abs(player.transform.position.y - transform.position.y) < movementRange)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y); 
            moveInput = 0;
        }
        
        animator.SetFloat("AnimationSpeed", Mathf.Abs(moveInput));
    }


    void flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }
}