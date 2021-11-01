using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour //attached to camera
{
    private float maxHealth = 100;
    [HideInInspector]
    public float currentHealth;

    private Animator anim;
    private Rigidbody2D rb;

    private ButtonMovement buttonMovement; //for push direction

    private GameObject enemyHealthBar;
    private Transform bar; //health bar
    private Animator healthBarAnim; //for fading out healthbar when dead

    void Awake()
    {
       LevelManager.enemyHealthBar = gameObject; //sets reference for health bar 
    }

    void Start()
    {
        enemyHealthBar = LevelManager.enemyHealthBar; //create healthbar object 
        bar = enemyHealthBar.transform.Find("Bar"); //finds enemy healthbar fill
        healthBarAnim = enemyHealthBar.GetComponent<Animator>(); //for fading out healthbar when dead NOTWORKINGNOTWORKINGNOTWORKING :(

        rb = LevelManager.enemyPrefabInstance.GetComponent<Rigidbody2D>(); //get enemy rb
        anim = LevelManager.enemyPrefabInstance.transform.GetChild(0).GetComponent<Animator>(); //get enemy anim from graphics gameObject

        buttonMovement = GameObject.Find("Main Camera").GetComponent<ButtonMovement>(); //get button movement for push direction
        if(buttonMovement == null)
        {
            Debug.LogWarning("NO BUTTON MOVEMENT SCRIPT ON CAMERA");
        }

        currentHealth = maxHealth; //set health to max health (100)
    }

    public void TakeDamage(float damageTaken)
    {
        currentHealth = currentHealth - damageTaken; //decrease health
        bar.localScale = new Vector3 (currentHealth / 100, 1f); //transform bar scale by: X: current health / 100 (to get health as .0f - bar scales from 1 to 0) Y: 1f (so bar doesnt shrink vertically)
        
        if(bar.localScale.x <= 0)
        {
            bar.localScale = new Vector3 (0, 1); //ensure bar does not display negatively if damage input is over max health
        }

        anim.Play("damagedAnim"); //play damage anim;

        PushInCorrectDirection(); //pushes enemy slightly 

        if(currentHealth <= 0) //if health = 0
        {
            EnemyDie(); //call death method
            currentHealth = 0; //health is 0 
        }
    }

        public void PushInCorrectDirection()
    {
        float hitDirection = buttonMovement.flipDirection;//gets direction as float (-1/1) so enemy gets pushed in the right direction

        if (hitDirection == 1)
        {
            rb.velocity = new Vector2 (8, 0); //push character away from player slightly when hit
        }
        if (hitDirection == - 1)
        {
            rb.velocity = new Vector2 (-8, 0); //push character away from player slightly when hit in other direction
        }
    }

        public void EnemyDie()
    {
        Debug.Log("Enemy dead!");
        anim.Play("deathAnim"); //play death anim;
        healthBarAnim.SetTrigger("hasDied"); //fade out enemy UI

        if (healthBarAnim.GetCurrentAnimatorStateInfo(0).IsName("fadeOut"))
        {
            Debug.Log("fading...");
        }

        Destroy(transform.parent.parent.parent.gameObject, 0.7f); //destroy parent of parent of parent (EnemyPathfindingLogic) after anim completed (after 0.7 seconds - length of anim)
    }
}
