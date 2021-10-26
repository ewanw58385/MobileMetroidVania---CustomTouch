using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCharacterCollision : MonoBehaviour
{
    public GameObject enemyPrefab;

    void Start()
    {
        enemyPrefab = GameObject.Find("EnemyPathfindingLogic");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D enemyPrefabCollider = enemyPrefab.GetComponent<CapsuleCollider2D>();
        Physics2D.IgnoreCollision(enemyPrefabCollider, GetComponent<CapsuleCollider2D>());
    }
}
