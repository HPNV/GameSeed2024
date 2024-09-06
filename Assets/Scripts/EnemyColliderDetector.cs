using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class EnemyColliderDetector : EnemyDetectorService
{
    [SerializeField]
    private Collider2D enmCollider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>();
        
        if (enemy is not null)
        {
            Enemies.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>();
        
        if (enemy is not null)
        {
            Enemies.Remove(enemy);
        }
    }
}
