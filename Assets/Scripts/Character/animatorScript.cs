using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorScript : MonoBehaviour
{
    public ButtonMovement buttonMovement;
    Animator anim;
    Rigidbody2D rb;

    Vector2 direction; 
    Vector2 force;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 direction = new Vector2(buttonMovement.direction.x, buttonMovement.direction.y);//gets direction Vector2 from button script to flip sprite 
        //Debug.Log("fd: " + direction);

        Flip(direction);
        SetSpeedForAnim();
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
    
    void SetSpeedForAnim()
    {
        float speed = rb.velocity.x;
        //Debug.Log(speed);

        if(speed < 0 )
        {
            speed = -speed;
             anim.SetFloat("speed", speed );
        }

    }
}
