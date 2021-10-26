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

    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        flipDirection = buttonMovement.flipDirection;//gets direction as float (-1/1) from button script to flip sprite 
        Flip(flipDirection);

        FreezeWhileAttacking();
        TransitionToIdleFromFalling();
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

    public void StoreAttackInArray()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer); //creates array to store attack

        foreach(Collider2D enemy in enemiesHit) //for each hit
        {
            Debug.Log("hit " + enemy.name + "!"); //apply damage
            enemy.GetComponent<EnemyHealth>().TakeDamage();
        }
    }

    void OnDrawGizmosSelected() //to display the attack range in scene view 
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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
}
