using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    private float moveSpeed = 2.75f;
    private int lastDirection;

    Vector2 direction;

    public Animator anim;
    public Rigidbody2D rb;

    void Update()
    {  
        FacingIdleManager();

        //transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);
    
        //anim.SetFloat("horizontal", direction.x); //for blendtree left n right
        //anim.SetFloat("speed", moveSpeed); //to check if is moving 
    }  

    void FixedUpdate()
    {
       //transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);
    }
    
    void FacingIdleManager()
    {

        Debug.Log(direction);
        if (direction.x > 0) //If statement to set the correct idle animation (idle right, left) based off last direction.
        
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        }
        if (direction.x < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

}
