using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    //Awake is called before start
    void Awake()
    {
      LevelManager.player = gameObject; //set reference for LevelManager   
      LevelManager.playerAttackPoint = transform.GetChild(1).gameObject; //set reference for attackPoint GameObject
    }
}
