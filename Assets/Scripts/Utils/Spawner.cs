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
    [SerializeField] private float spawnRateIncrease = 10000.99f;
    [SerializeField] private float resetThreshold = 0.5f;
    private float spawnTimer = 0f;

    // Update is called once per frame

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            spawnTimer = 0f;
            SpawnEnemy();
            spawnRate *= spawnRateIncrease;
        }

        if(spawnRate < resetThreshold)
        {
            spawnRate = 3f;
            resetThreshold -= 0.05f;
        }
        
        if(Input.GetKeyDown(KeyCode.P))
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = Random.Range(spawnRadius * -1, spawnRadius);
        int enemyGacha = Random.Range(0, 100);
        
        if (enemyGacha < 40)
        {
            SingletonGame.Instance.EnemyManager.Spawn(EnemyType.Ranged, spawnPosition);
        }
        else if (enemyGacha < 80)
        {
            SingletonGame.Instance.EnemyManager.Spawn(EnemyType.Explosive, spawnPosition);
        }
        else
        {
            SingletonGame.Instance.EnemyManager.Spawn(EnemyType.Melee, spawnPosition);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
