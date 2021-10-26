using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float maxHealth = 100;
    [HideInInspector]
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage()
    {
        currentHealth = currentHealth - 10;
        Debug.Log(currentHealth);

        //play damage anim;

        if(currentHealth <= 0)
        {
            EnemyDie();
            currentHealth = 0;
        }
    }

    public void EnemyDie()
    {
        //play death anim;
        Debug.Log("Enemy dead!");
    }

}
