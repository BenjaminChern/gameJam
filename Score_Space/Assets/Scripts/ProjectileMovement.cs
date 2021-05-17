using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{

    private float moveInput;
    public float speed;
    public Animator animator;
    private bool facingRight;

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

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        transform.position = new Vector2(transform.position.x + moveInput * speed, transform.position.y);
    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }
}
