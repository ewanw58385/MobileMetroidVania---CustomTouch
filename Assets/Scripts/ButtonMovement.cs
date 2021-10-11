using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMovement : MonoBehaviour
{

    public float moveSpeed = 300; 
    public GameObject Player;

    private Rigidbody2D rb; 
    private float screenWidth;

    void Start()
    {
        screenWidth = Screen.width; 
        rb = Player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       int i = 0;

        while (i < Input.touchCount) //loops for every touch
        {
            if (Input.GetTouch(i).position.x > screenWidth / 2) //if touch is on left half of screen
            {
                MoveCharacter(1f); //pass horizontal value as float
                Debug.Log("moving char left");
            }

            if (Input.GetTouch(i).position.x < screenWidth / 2)
            {
                MoveCharacter(-1f);
                Debug.Log("moving char right");
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
