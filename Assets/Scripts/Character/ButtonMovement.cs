using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMovement : MonoBehaviour
{

    public float moveSpeed = 300;

    private Vector2 startPosition; //where you clicked
    private Vector2 movingPosition; //where you clicked and are moving 
    private Vector3 startPositionOnScreen; //where you clicked converted to position on screen
    private Vector3 movingPositionOnScreen; //where you clicked and are moving converted to position on screen
    private Vector3 joystickPositionScreen; //where joystick is converted to position on screen
    private Vector2 offsetPos; //where playing is touching - joystick positon
    private Vector2 offsetPosMove; //offsetPos clameped to 1 for a -1/1 integer for applying horizontal movement

    [HideInInspector] public Vector2 direction; //public direction vector for animation script to access (for flipping sprite)
    
    [HideInInspector] public bool stoppedMoving;

    public GameObject Player;
    public GameObject joystickSprite;
    private Vector2 joystickSpritePos;
    public Transform joystickHandle;
    public Camera cam;

    private Rigidbody2D rb; 
    private float screenWidth;

    void Start()
    {
        screenWidth = Screen.width; 
        rb = Player.GetComponent<Rigidbody2D>();

        stoppedMoving = true; //so idle anim is first played in anim script

        //Vector2 joystickSpritePos = new Vector2(joystickHandle.position.x, joystickHandle.position.y); //gets initial position of joystick handle for sprite to move around 
    }

    void FixedUpdate()
    {
        ManageTouches();
        PreventOutOfBoundsTouch();

        //Debug.Log("is grounded = " + GroundCheck());
    }

    private void ManageTouches()
    {
        int i = 0;

        while (i < Input.touchCount) //loops for every touch
        {
            if (Input.GetTouch(i).position.x < screenWidth / 2) //if touch is on left half of screen
            {
                Touch touch = Input.GetTouch(0); //instantiates new touch (for each touch)

                switch (touch.phase)
                {
                    case TouchPhase.Began:

                        startPosition = touch.position; //stores the first touch in pixels 
                        startPositionOnScreen = cam.ScreenToWorldPoint(startPosition); //converts touch position in pixels to touch position on screen

                        stoppedMoving = false; 

                        break;

                    case TouchPhase.Moved:

                        movingPosition = touch.position;  //converts current position while moving in pixels 
                        movingPositionOnScreen = cam.ScreenToWorldPoint(movingPosition); //converts touch position in pixels to touch position on screen 

                        stoppedMoving = false;
                        
                        break;

                    case TouchPhase.Stationary:

                        movingPosition = touch.position;
                        movingPositionOnScreen = cam.ScreenToWorldPoint(movingPosition);

                        stoppedMoving = false;

                        break;

                    case TouchPhase.Ended:

                        //joystickSprite.transform.position = new Vector2(joystickSpritePos.x, joystickSpritePos.y); //resets joystick sprite position
                        //Debug.Log("joystick reset");

                        stoppedMoving = true; //for setting velocity to 0 in MoveCharacter() method (to prevent sliding)
                        break;
                }

                Vector2 joystickPosition = new Vector2(joystickHandle.position.x, joystickHandle.position.y); //Gets position of handle as Vector2    
                joystickPositionScreen = cam.ScreenToWorldPoint(joystickPosition); //convert joystick position in world to position on screen

                offsetPos = joystickPositionScreen - movingPositionOnScreen; //calculates the difference between where the player is touching + where the gameobject is 
                Vector2 offsetPosMove = Vector2.ClampMagnitude(offsetPos, 1); //new Vector2 clamped from -1 to 1

                MoveCharacter(-offsetPosMove.x); //move character by clamped vector
            }

            i++;
        }
    }

    private void PreventOutOfBoundsTouch()
    {
        int i = 0;

        while (i < Input.touchCount) //loops for every touch
        {
                Touch touch = Input.GetTouch(0); //instantiates new touch (for each touch)

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                    
                    startPosition = touch.position;

                    break;

                    case TouchPhase.Moved:

                        movingPosition = touch.position;  //converts current position while moving in pixels  

                        if ((movingPosition.x > screenWidth / 2) && (startPosition.x < screenWidth / 2) ) //if touch moves out of bounds and beginning of touch was on the left 
                        {
                            Debug.Log("touch moved out of bounds");
                            rb.velocity = Vector2.zero;
                        }

                        break;
                }         
            i++;
        }
    }

    private bool GroundCheck()
    {
        return GameObject.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;
    }

    public void MoveCharacter(float directionHori)
    {
        var velocity = rb.velocity; //save velocity 
        velocity.x = directionHori * moveSpeed; //pass direction.x * moveSpeed to velocity.x
        rb.velocity = velocity; //set velocity on rb

        if (stoppedMoving && GroundCheck() ) //because moving by velocity creates sliding, I set velocity to 0 when player is not touching the screen. groundcheck to make sure player can't stop falling midair by stopping velocity  
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = velocity; //apply velocity to player
            stoppedMoving = false;
        }

        direction = new Vector2 (directionHori, 0); //public direction Vector for setting flip in anim controller

        if (Player.GetComponent<animatorScript>().freezeWhileAttacking == true) //so player cannot move while attacking 
        {
            rb.velocity = Vector2.zero;
        }
    }
}
