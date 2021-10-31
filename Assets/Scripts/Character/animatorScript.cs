using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorScript : MonoBehaviour
{
    public ButtonMovement buttonMovement;
    Animator anim;
    Rigidbody2D rb;

    [HideInInspector]
    public float flipDirection;
    [HideInInspector]
    public bool freezeWhileAttacking = false;

    //public Transform LevelManager.playerAttackPoint;
    public float attackRange;
    public LayerMask enemyLayer;

    Animator enemyAnim;

    public float playerDamage = 25;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        //GameObject enemyPrefab = LevelManager.enemyPrefab;
        enemyAnim = LevelManager.enemyPrefab.transform.GetChild(0).GetComponent<Animator>(); //Referances the static prefab from level manager, gets child of prefab, gets animator component from child
    }

    void Update()
    {
        flipDirection = buttonMovement.flipDirection;//gets direction as float (-1/1) from button script to flip sprite 
        Flip(flipDirection);

        FreezeWhileAttacking();
        TransitionToIdleFromFalling();

        //LevelManager.TestStaticMethod();
    }

    void FixedUpdate()
    {
        SetMovementBoolForAnim();
    }

    private bool GroundCheck()
    {
        return GameObject.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;
    }

    void Flip(float flipDirection)
    {
        if (flipDirection < 0) //move left
        {
            transform.localScale = new Vector3 (1f, 1f, 1f);
        }

        if (flipDirection > 0) //move right
        {
            transform.localScale = new Vector3 (-1f, 1f, 1f); //flip 
        }
    }
    
    void SetMovementBoolForAnim()
    {
        bool stoppedMoving = buttonMovement.stoppedMoving;

        if(stoppedMoving)
        {
            anim.SetBool("isMoving", false); //idle anim
        }
        else
        {
            anim.SetBool("isMoving", true); //running anim
        }
    }

    public void PlayAttackFromButton()
    {
        anim.SetTrigger("attack"); //playAttackAnim
    }

    public void StoreAttackInArray() //called from animation event 
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(LevelManager.playerAttackPoint.transform.position, attackRange, enemyLayer); //creates array to store attack

        foreach(Collider2D enemy in enemiesHit) //for each enemy hit
        {
            if (!enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("deathAnim")) //if death animation IS NOT playing, take damage (otherwize attackAnim restarts when attacked if attacked while dying)
            {
            Debug.Log("hit " + enemy.name + "!"); //apply damage
            enemy.GetComponent<EnemyHealth>().TakeDamage(playerDamage); //calls take damage script passing in damage output (10)
            }
        }
    }

    public void FreezeWhileAttacking()
    {
         if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")) //if attack is playing
         {
            freezeWhileAttacking = true; //freeze the player (done in button script)
         }
         else
         {
            freezeWhileAttacking = false; //unfreeze if not attacking
         }
    }

    public void TransitionToIdleFromFalling()
    {
        /*if (rb.velocity.y < -0.1) //if falling
        {
            anim.Play("idle");
        }*/
    }

        void OnDrawGizmosSelected() //to display the attack range in scene view 
    {
        if (LevelManager.playerAttackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(LevelManager.playerAttackPoint.transform.position, attackRange);
    }
}
