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
    public float speed = 1;
    public float health = 3;

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
        rb = GetComponent<Rigidbody2D>();

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
        

        if ((player.transform.position.x - rb.position.x) > 5)
        {
            if (facingRight == false)
            {
                flip();//attack
            }
        }
        if ((player.transform.position.x - rb.position.x) < 5)
        {
            if (facingRight == true)
            {
                flip();
            }
            run();
        }
        if ((player.transform.position.x - rb.position.x) > -5)
        {
            if (facingRight == false)
            {
                flip(); //attack
            }
        }
        if ((player.transform.position.x - rb.position.x) < -5)
        {
            if (facingRight == true)
            {
                flip();
            }
            run();
        }

        if (Time.time > nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;
            animator.SetTrigger("Attack3");
            GameObject projectile = Instantiate(arrow, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity);
            if (facingRight)
            {
                projectile.GetComponent<ProjectileMovement>().setMoveInput(1);
            }
            else
            {
                projectile.GetComponent<ProjectileMovement>().setMoveInput(-1);
            }
            Destroy(projectile, 1.5f);
            

        }
    }
    private void run()
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        animator.SetFloat("AnimationSpeed", Mathf.Abs(moveInput));
    }

    public void getHit(int damage)
    {
        health -= damage;
        //Debug.Log("took " + damage + "damage");
    }


    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

    }
}