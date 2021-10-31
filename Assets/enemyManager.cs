using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    //Awake is called before start
    void Awake()
    {
        LevelManager.enemyPrefab = gameObject; //set reference for levelManager
    }
}
