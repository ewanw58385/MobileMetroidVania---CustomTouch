using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    private float maxHealth = 100;
    [HideInInspector]
    public float currentHealth;

    private Animator anim;
    private Rigidbody2D rb;

    private ButtonMovement buttonMovement;

    public sliderScript healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; //set health to max health
        healthBar.SetMaxHealth(maxHealth); //call maxhealth() method for slider

        anim = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();


        buttonMovement = GameObject.Find("Main Camera").GetComponent<ButtonMovement>();
        if(buttonMovement == null)
        {
            Debug.LogWarning("NO BUTTON MOVEMENT SCRIPT ON CAMERA");
        }
    }

    public void TakeDamage(float damageTaken)
    {
        float hitDirection = buttonMovement.flipDirection;//gets direction as float (-1/1) so enemy gets pushed in the right direction

        currentHealth = currentHealth - damageTaken; //decrease health
        healthBar.SetHealth(currentHealth); //pass new current health to slider

        Debug.Log(currentHealth);


        anim.Play("damagedAnim"); //play damage anim;

        if (hitDirection == 1)
        {
            rb.velocity = new Vector2 (8, 0); //push character away from player slightly when hit
        }
        if (hitDirection == - 1)
        {
            rb.velocity = new Vector2 (-8, 0); //push character away from player slightly when hit in other direction
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
        anim.Play("deathAnim");

        Destroy(transform.parent.parent.gameObject, 0.7f); //destroy parent of parent (EnemyPathfindingLogic) after anim completed (after 0.7 seconds - length of anim)
    }

}
