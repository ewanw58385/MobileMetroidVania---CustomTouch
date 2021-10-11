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


    public Joystick joystick;
    public float joystickSensitivity = 0.2f;

    void Update()
    {  
        FacingIdleManager();
        SetJoystickSensitivity();

        direction = new Vector2(joystick.Horizontal, 0);

        transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);
    
        anim.SetFloat("horizontal", direction.x); //for blendtree left n right
        anim.SetFloat("speed", moveSpeed); //to check if is moving 


    }  

    void FixedUpdate()
    {
       transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);
    }
    
    void FacingIdleManager()
    {
        if (direction.x > 0 || direction.x < 0) //If statement to set the correct idle animation (idle right, left) based off last direction.
        
        {
            anim.SetFloat("lastHori", direction.x);

        }
    }

    void SetJoystickSensitivity()
    {
        if ((joystick.Horizontal <= joystickSensitivity) && (joystick.Horizontal >= -joystickSensitivity)) //if statement to set sensitivity of joystick
        {
            moveSpeed = 0f;
        }
        else 
        {
            moveSpeed = 2.75f;
        }
    }

}
