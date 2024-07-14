using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 3;
    public Animator animator;
    private bool firstDeath = true; 

    private void Start()
    {

    }

    private void FixedUpdate()
    {

    }

    private void Update()
    {

    }
    public void getHit(int damage)
    {
        health -= damage;
        if (health > 0)
        {
            animator.SetTrigger("Hurt");
        }
        if (health <= 0 && firstDeath)
        {
            animator.SetTrigger("Die");
            firstDeath = false;
            this.gameObject.GetComponent<MeleeEnemyAI>().die();

        } 
        else if(health <= 0) {
            Destroy(this.gameObject);
        }
    }
}
