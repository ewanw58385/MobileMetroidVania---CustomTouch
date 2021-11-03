using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    public static GameObject player;

    [HideInInspector]
    public GameObject enemyPrefabInstance;

    private Vector2 spawnPosition1;
    private Vector2 spawnPosition2;

    public static List<GameObject> enemyInstancesList; //define List

    void Awake()
    {
        List<GameObject> enemyInstancesList = new List<GameObject>(); //set list
    }

    void Start()
    {
        spawnPosition1 = new Vector2(11.7f, -1.4f);
        spawnPosition2 = new Vector2(4.85f, -1.4f);
        
        SpawnEnemy(spawnPosition1, Quaternion.identity);
        //SpawnEnemy(spawnPosition2, Quaternion.identity); //EACH NEW INSTANTIATION OVERRIDES THE PREVIOUS IN LEVEL MANAGER (static GO can only hold 1 reference) SO THIS ISN'T WORKING 
    }

    void SpawnEnemy(Vector2 position, Quaternion rotation)
    {
        enemyPrefabInstance = Instantiate(enemyPrefab, position, rotation); //instantiate enemy, at position, with 0 rotation
    }
}
