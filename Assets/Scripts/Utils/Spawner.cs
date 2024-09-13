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

        // Manual spawn for testing
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        // Update enemy count

        // Random spawn position within radius
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = Random.Range(spawnRadius * -1, spawnRadius);

        // Decide enemy type using random
        int enemyGacha = Random.Range(0, 100);

        var spawnedEnemyType = new List<EnemyName>();
        
        if (enemyGacha < 40)
        {
            spawnedEnemyType.AddRange(new List<EnemyName> { EnemyName.SlimeSpitter, EnemyName.GlobLobber, EnemyName.GelGrenadier });
        }
        else if (enemyGacha < 80)
        {
            spawnedEnemyType.AddRange(new List<EnemyName> { EnemyName.SludgeGrunt, EnemyName.SwiftSlimer, EnemyName.GooGuardian });
        }
        else
        {
            spawnedEnemyType.AddRange(new List<EnemyName> { EnemyName.BlastBlob, EnemyName.GoliathOoze });
        }
        
        SingletonGame.Instance.EnemyManager.Spawn(spawnedEnemyType[Random.Range(0, spawnedEnemyType.Count)], spawnPosition,multiplier);
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
