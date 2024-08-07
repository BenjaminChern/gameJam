using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int health = 6;
    public Animator animator;
    private bool firstDeath;

    public Canvas heartCanvas;

    private void Start()
    {
        firstDeath = true;
        
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
        heartCanvas.GetComponent<HeartScript>().healthSet(health);
        this.GetComponent<PlayerMovement>().gotHit();
        if (health > 0)
        {
            animator.SetTrigger("Hurt");
        }
        //Debug.Log("player took " + damage + "damage");
        if(health <= 0 && firstDeath)
        {
            animator.SetTrigger("Die");
            firstDeath = false;
            heartCanvas.GetComponent<DarkScreen>().darken();
            this.GetComponent<PlayerMovement>().setAlive(false);
        }
    }
}
