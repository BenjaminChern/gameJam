using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float moveInput;
    public float speed;
    public Animator animator;
    private bool facingRight;
    public LayerMask playerLayer;
    public LayerMask enemyLayer;
    public LayerMask backgroundLayer;
    private float currentTime;
    public float maxDistance;


    public GameObject player;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {


        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentTime != 0)
        {
            print(Time.time);
            print(currentTime + "current time");
            if (Time.time > currentTime)
            {
                print("destroyed");
                Destroy(this.gameObject);
            }
        }
        /*else if(Time.time > maxDistance)
        {
            Destroy(this);
        }*/
        else
        {

            rb.velocity = new Vector2((speed * moveInput), 0);
            print(moveInput * speed);
            Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, 0.1f, playerLayer);
            if (hitPlayer != null)
            {
                animator.SetTrigger("Contact");
                moveInput = 0;
                rb.velocity = new Vector2(0, 0);
                currentTime = Time.time + .4f;
               
            }
            Collider2D hitBackground = Physics2D.OverlapCircle(transform.position, 0.1f, backgroundLayer);
            if (hitBackground != null)
            {
                Destroy(this.gameObject);

            }

            /*Collider2D hitEnemy = Physics2D.OverlapCircle(transform.position, 0.1f, enemyLayer);
            if (hitEnemy != null)
            {
                animator.SetTrigger("Contact");
                currentTime = Time.time + .4f;
            }*/
        }
        
    }

    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

    }
    public void setMoveInput(float input)
    {
        moveInput = input;
    }
}
