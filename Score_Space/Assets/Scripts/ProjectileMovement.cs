using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{

    private float moveInput;
    public float speed;
    public Animator animator;
    private bool facingRight;
    public LayerMask playerLayer;
    public LayerMask enemyLayer;
    private float currentTime;
    public float maxDistance;


    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        moveInput = 1;
        facingRight = true;
        if ((player.transform.position.x - transform.position.x) < 0)
        {
            moveInput = -1;
            flip();
        }
        currentTime = 0;

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

            transform.position = new Vector2(transform.position.x + moveInput * speed, transform.position.y);
            Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, 0.1f, playerLayer);
            if (hitPlayer != null)
            {
                animator.SetTrigger("Contact");
                currentTime = Time.time + .4f;
                


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
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }
}
