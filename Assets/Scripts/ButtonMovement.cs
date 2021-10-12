using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMovement : MonoBehaviour
{

    public float moveSpeed = 300;

    private Vector2 touchPosition;

    public GameObject Player;
    public GameObject joystickHandle;
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
       int i = 0;

        while (i < Input.touchCount) //loops for every touch
        {
            if (Input.GetTouch(i).position.x > screenWidth / 2) //if touch is on left half of screen
            {
                Touch touch = Input.GetTouch(0); //instantiates new touch (for each touch)

                touchPosition = new Vector2(touch.position.x, touch.position.y); //passes in touch position to vector2 
                Debug.Log(touchPosition); //debug
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
