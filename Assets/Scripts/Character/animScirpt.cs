using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animScirpt : MonoBehaviour
{
 
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void IsAttacking()
    {
        anim.SetFloat("isAttacking", 1);
        anim.SetFloat("finishedAttacking", 0);
    }

    public void FinishedAttacking()
    {
        anim.SetFloat("finishedAttacking", 1);
        anim.SetFloat("isAttacking", 0);
    }
}
