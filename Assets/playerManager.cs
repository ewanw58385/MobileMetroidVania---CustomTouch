using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    void Start()
    {
      LevelManager.player = gameObject; //set reference for LevelManager   
      LevelManager.playerAttackPoint = transform.GetChild(1).gameObject; //set reference for attackPoint GameObject
    }
}
