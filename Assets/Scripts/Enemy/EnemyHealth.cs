using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float maxHealth = 100;
    [HideInInspector]
    public float currentHealth;

    private Animator anim;
    private Rigidbody2D rb;

    private ButtonMovement buttonMovement;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        
        anim = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();

        buttonMovement = GameObject.Find("Main Camera").GetComponent<ButtonMovement>();
    }

    public void TakeDamage()
    {
        float hitDirection = buttonMovement.flipDirection;//gets direction as float (-1/1) so enemy gets pushed in the right direction

        currentHealth = currentHealth - 10; //decrease health
        Debug.Log(currentHealth);

        anim.Play("Damaged"); //play damage anim;

        if (hitDirection == 1)
        {
            rb.velocity = new Vector2 (5, 0);
        }
        if (hitDirection == -1)
        {
            rb.velocity = new Vector2 (-5, 0);
        }

        if(currentHealth <= 0) //if health = 0
        {
            EnemyDie(); //call death method
            currentHealth = 0; //health is 0 
        }
    }

    public void EnemyDie()
    {
        //play death anim;
        Debug.Log("Enemy dead!");
    }

}
