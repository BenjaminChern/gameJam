using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float health = 10;
    public Animator animator;
    private bool firstDeath;
    public GameObject noHeart;
    public GameObject halfHeart;
    public GameObject Heart;

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
        if(health > 0)
        {
            animator.SetTrigger("Hurt");
            Debug.Log("multiple hurt");
        }
        //Debug.Log("player took " + damage + "damage");
        if(health <= 0 && firstDeath)
        {
            animator.SetTrigger("Die");
            firstDeath = false;
        }
    }
}
