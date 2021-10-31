using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    void Awake()
    {
        Instantiate(enemyPrefab, new Vector2(35, -11), Quaternion.identity); //instantiate enemy, at position, with 0 rotation
    }
}
