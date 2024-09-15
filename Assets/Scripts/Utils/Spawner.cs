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
    [SerializeField] private float initialSpawnRate = 5f;   // Starting spawn rate
    [SerializeField] private float minSpawnRate = 0.5f;     // Minimum allowed spawn rate
    [SerializeField] private float spawnRateDecrease = 0.9f;// How much spawn rate decreases each spawn
    [SerializeField] private float spawnRadius = 1f;
    private float spawnRate;
    private float spawnTimer = 0f;
    private float multiplier = 1f;

    private void Start()
    {
        spawnRate = initialSpawnRate;
        IncreaseStrengthOverTime();
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        // Only spawn if we are under the max enemy limit
        if (spawnTimer >= spawnRate)
        {
            spawnTimer = 0f;
            SpawnEnemy();

            // Gradually decrease the spawn rate, but not below minSpawnRate
            spawnRate = Mathf.Max(spawnRate * spawnRateDecrease, minSpawnRate);
        }
        
    }

    private void SpawnEnemy()
    {
        // Update enemy count

        // Random spawn position within radius
        Vector2 spawnPosition = transform.position + Random.onUnitSphere * spawnRadius;

        // Decide enemy type using random
        int enemyGacha = Random.Range(0, 100);

        var spawnedEnemyType = new List<EnemyName>();

        var time = SingletonGame.Instance.homeBase.time;
        if (enemyGacha < 40)
        {
            spawnedEnemyType.Add(EnemyName.SlimeSpitter);
            
            if (enemyGacha - (time / 100) < 30)
            {
                spawnedEnemyType.Add(EnemyName.SlimeSpitter);
            }
            if (enemyGacha - (time / 100) < 20)
            {
                spawnedEnemyType.Add(EnemyName.GlobLobber);
            }

        }
        else if (enemyGacha < 95)
        {
            spawnedEnemyType.Add(EnemyName.SludgeGrunt);
            
            if (enemyGacha - (time / 100) < 80)
            {
                spawnedEnemyType.Add(EnemyName.SludgeGrunt);
            }
            
            if (enemyGacha - (time / 100) < 60)
            {
                spawnedEnemyType.Add(EnemyName.SwiftSlimer);
            }
        }
        else
        {
            spawnedEnemyType.Add(EnemyName.BlastBlob);
            
            if (enemyGacha - (time / 100) < 92)
            {
                spawnedEnemyType.Add(EnemyName.GoliathOoze);
            }
        }
        
        SingletonGame.Instance.EnemyManager.Spawn(spawnedEnemyType[Random.Range(0, spawnedEnemyType.Count)], spawnPosition, multiplier);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    private IEnumerator IncreaseStrengthOverTime()
        {
            while (true)
            {
                yield return new WaitForSeconds(30f); // Wait 30 seconds
                multiplier += 0.2f; // Multiply strength by 1.2 (increase by 20%)
            }
        }
}
