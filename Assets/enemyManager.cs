using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.enemyPrefab = gameObject; //(referancing EnemyPathfindingLogic - parent gameObject)
    }
}
