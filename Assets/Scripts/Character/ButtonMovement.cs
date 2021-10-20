using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMovement : MonoBehaviour
{

    public float moveSpeed = 300;

    private Vector2 startPosition;
    private Vector2 movingPosition;
    private Vector3 startPositionOnScreen;
    private Vector3 movingPositionOnScreen;
    private Vector3 joystickPositionScreen;
    private Vector2 offsetPos;
    private Vector2 offsetPosMove;

    public Vector2 direction; 

    public GameObject Player;
    public GameObject joystickSprite;
    public Vector2 joystickSpritePos;
    public Transform joystickHandle;
    public Camera cam;

    private Rigidbody2D rb; 
    private float screenWidth;

    void Start()
    {
        screenWidth = Screen.width; 
        rb = Player.GetComponent<Rigidbody2D>();

        //Vector2 joystickSpritePos = new Vector2(joystickHandle.position.x, joystickHandle.position.y); //gets initial position of joystick handle for sprite to move around 
    }

    void FixedUpdate()
    {
        ManageTouches();
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

                        break;

                    case TouchPhase.Moved:

                        movingPosition = touch.position;  //converts current position while moving in pixels 
                        movingPositionOnScreen = cam.ScreenToWorldPoint(movingPosition); //converts touch position in pixels to touch position on screen 

                        break;

                    case TouchPhase.Stationary:

                        movingPosition = touch.position;
                        movingPositionOnScreen = cam.ScreenToWorldPoint(movingPosition);

                        break;

                    case TouchPhase.Ended:

                        //joystickSprite.transform.position = new Vector2(joystickSpritePos.x, joystickSpritePos.y); //resets joystick sprite position
                        Debug.Log("joystick reset");

                        break;
                }


                Vector2 joystickPosition = new Vector2(joystickHandle.position.x, joystickHandle.position.y); //Gets position of handle as Vector2 
                joystickPositionScreen = cam.ScreenToWorldPoint(joystickPosition); //convert joystick position in world to position on screen

                offsetPos = joystickPositionScreen - movingPositionOnScreen; //calculates the difference between where the player is touching + where the gameobject is 

                Vector2 offsetPosMove = Vector2.ClampMagnitude(offsetPos, 1); //new Vector2 clamped from -1 to 1
                //Debug.Log("Moved Character by: " + -offsetPosMove);

                MoveCharacter(-offsetPosMove.x); //move character by clamped vector
            }
            i++;
        }
    }

    public void MoveCharacter(float directionHori)
    {
        direction = new Vector2 (directionHori, 0); //new vector2 using passed x value (1 or -1). //BUG - passing in 0 as Y axis overrides gravity while player is moving. 
        //passing in -1 or other negative integer for Y works but speeds falling while moving and not moving differ since gravity is overrided.

        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime); //apply movement 
    }
}
