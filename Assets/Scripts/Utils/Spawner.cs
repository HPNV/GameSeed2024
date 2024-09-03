using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Manager;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 5f;
    [SerializeField] private float spawnRadius = 1f;
    [SerializeField] private float spawnRateIncrease = 0.99f;
    private EnemyManager enemyManager = new();
    private float spawnTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        enemyManager.Initialize();
    }

    private void FixedUpdate()
    {
        spawnTimer += Time.fixedDeltaTime;
        if (spawnTimer >= spawnRate)
        {
            spawnTimer = 0f;
            SpawnEnemy();
            Debug.Log($"Spawned enemy {spawnRate}");
            spawnRate *= spawnRateIncrease;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = Random.Range(spawnRadius * -1, spawnRadius);
        int enemyGacha = Random.Range(0, 100);
        
        if (enemyGacha < 40)
        {
            enemyManager.Spawn(EnemyType.Ranged, spawnPosition);
        }
        else if (enemyGacha < 80)
        {
            enemyManager.Spawn(EnemyType.Explosive, spawnPosition);
        }
        else
        {
            enemyManager.Spawn(EnemyType.Melee, spawnPosition);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
