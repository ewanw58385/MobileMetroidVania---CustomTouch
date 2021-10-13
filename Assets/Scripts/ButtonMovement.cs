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

    public GameObject Player;
    public Transform joystickHandle;
    public Camera cam;

    private Rigidbody2D rb; 
    private float screenWidth;

    void Start()
    {
        screenWidth = Screen.width; 
        rb = Player.GetComponent<Rigidbody2D>();
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

                        Debug.Log("Moved to positon: " + movingPositionOnScreen + "On screen");
                        break;
                }


                Vector2 joystickPosition = new Vector2(joystickHandle.position.x, joystickHandle.position.y); //Gets position of handle as Vector2 
                //Debug.Log(joystickPosition);
                joystickPositionScreen = cam.ScreenToWorldPoint(joystickPosition); //convert joystick position in world to position on screen
                //Debug.Log(joystickPositionScreen);
            }

            i++;
        }
    }

    private void MoveCharacter(float directionHori)
    {
        Vector2 direction = new Vector2 (directionHori, 0); //new vector2 using passed x value (1 or -1). //BUG - passing in 0 as Y axis overrides gravity while player is moving. 
        //passing in -1 or other negative integer for Y works but speeds falling while moving and not moving differ since gravity is overrided. 

        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime); //apply movement 
    }
}
