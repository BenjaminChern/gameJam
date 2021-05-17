using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool facingRight = true;
    public GameObject player;
    public float attackRange; 

    private float moveInput;
    private float speed;
    private float health;

    public float attackCooldown;
    private float nextAttackTime;

    public GameObject arrow;


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

        /*if (facingRight)
        {
            moveInput = 1;
        }
        else 
        {
            moveInput = -1;
        }*/
        

        if ((player.transform.position.x - rb.position.x) > .01)
        {
            if (facingRight == false)
            {
                flip();
            }
        }
        else if((player.transform.position.x - rb.position.x) < -.01)
        {
            if (facingRight)
            {
                flip();
            }
        }

        if (Time.time > nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;
            animator.SetTrigger("Attack3");
            
            Destroy(Instantiate(arrow, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity), 1f);

        }


        //rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        //animator.SetFloat("AnimationSpeed", Mathf.Abs(moveInput));
    }


    void flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }
}