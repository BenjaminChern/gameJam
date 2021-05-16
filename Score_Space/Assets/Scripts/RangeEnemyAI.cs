using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool facingRight = true;
    public Rigidbody2D player;

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
        if ((player.position.x - rb.position.x) >= 0)
        {
            if (facingRight == false)
            {
                flip();
            }
        }
        else
        {
            if (facingRight)
            {
                flip();
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
}