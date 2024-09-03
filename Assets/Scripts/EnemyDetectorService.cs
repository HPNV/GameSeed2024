using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public abstract class EnemyDetectorService : MonoBehaviour
{
    protected List<EnemyBehaviour> Enemies;

    private void Start()
    {
        Enemies = new List<EnemyBehaviour>();
    }

    public List<EnemyBehaviour> GetEnemiesInRange()
    {
        return Enemies;
    }
}
