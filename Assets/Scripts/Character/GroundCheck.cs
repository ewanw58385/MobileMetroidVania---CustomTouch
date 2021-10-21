using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    [HideInInspector] public bool isGrounded;
    [SerializeField] private LayerMask ground;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "floor") 
        {
            isGrounded = true;
        }
        //isGrounded = col != null && ((( 1 << col.gameObject.layer) & ground) != 0 ); //2nd way of detecting 
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        isGrounded = false;
    }
}
