using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private float spawnRadius = 1f;
    [SerializeField] private float spawnRateIncrease = 0.1f;
    private float spawnTimer = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        spawnTimer += Time.fixedDeltaTime;
        if (spawnTimer >= spawnRate)
        {
            spawnTimer = 0f;
            SpawnEnemy();
            spawnRate -= spawnRateIncrease;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = 0;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
