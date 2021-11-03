using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkIfHit : MonoBehaviour
{
    public Animator playerAnim;

    void Start()
    {
        playerAnim = GameManager.player.GetComponent<Animator>();
    }
        void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "attackPoint" && playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack") )
        {
            if (playerAnim.GetCurrentAnimatorStateInfo.length == 0.0f || playerAnim.GetCurrentAnimatorStateInfo.length == 0.4f)
            {
                print(col.gameObject.tag);
                //takeDamageMethod here
            }
        }
    } 
}
