using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    [HideInInspector]
    public GameObject enemyPrefabInstance;

    private Vector2 spawnPosition1 = new Vector2(11.7f, -1.4f);
    private Vector2 spawnPosition2 = new Vector2(4.85f, -1.4f);

    void Awake()
    {
        SpawnEnemy(spawnPosition1, Quaternion.identity);
        SpawnEnemy(spawnPosition2, Quaternion.identity); //EACH NEW INSTANTIATION OVERRIDES THE PREVIOUS IN LEVEL MANAGER (static GO can only hold 1 reference) SO THIS ISN'T WORKING 

    }

    void SpawnEnemy(Vector2 position, Quaternion rotation)
    {
        enemyPrefabInstance = Instantiate(enemyPrefab, position, rotation) as GameObject; //instantiate enemy, at position, with 0 rotation

        LevelManager.enemyPrefabInstance = enemyPrefabInstance.gameObject;
    }
}
