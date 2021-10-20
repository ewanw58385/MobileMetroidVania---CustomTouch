using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorScript : MonoBehaviour
{
    public ButtonMovement buttonMovement;
    Vector2 flipDirection; 

    void Update()
    {
        Vector2 flipDirection = new Vector2(buttonMovement.direction.x, buttonMovement.direction.y);//gets direction Vector2 from button script to flip sprite 
        Debug.Log("fd: " + flipDirection);

        Flip(flipDirection);
    }

    void Flip(Vector2 flipDirection)
    {
        if (flipDirection.x < 0) //move left
        {
            transform.localScale = new Vector3 (1f, 1f, 1f);
        }

        if (flipDirection.x > 0) //move right
        {
            transform.localScale = new Vector3 (-1f, 1f, 1f);
        }
    }
}
