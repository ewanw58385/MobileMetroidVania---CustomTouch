using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorScript : MonoBehaviour
{
    public ButtonMovement buttonMovement;
    Animator anim;
    Rigidbody2D rb;

    [HideInInspector] public bool freezeWhileAttacking = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 direction = buttonMovement.direction;//gets direction Vector2 from button script to flip sprite 
        Flip(direction);
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

    void Flip(Vector2 direction)
    {
        if (direction.x < 0) //move left
        {
            transform.localScale = new Vector3 (1f, 1f, 1f);
        }

        if (direction.x > 0) //move right
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

    public void Attack()
    {
        anim.SetTrigger("attack");
    }

    public void FreezeWhileAttacking()
    {
         if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
         {
            freezeWhileAttacking = true;
         }
         else
         {
            freezeWhileAttacking = false;
         }
    }

    public void TransitionToIdleFromFalling()
    {
        if (rb.velocity.y < -0.1 && !GroundCheck()) //if falling
        {
            anim.Play("idle");
        }
    }
}
