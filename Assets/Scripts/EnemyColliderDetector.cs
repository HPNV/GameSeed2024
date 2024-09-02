using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class EnemyColliderDetector : EnemyDetectorService
{
    [SerializeField]
    private Collider2D collider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>();
        if (enemy != null)
        {
            enemies.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>();
        if (enemy != null)
        {
            enemies.Remove(enemy);
        }
    }
}
