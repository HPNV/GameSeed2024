using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public abstract class EnemyDetectorService : MonoBehaviour
{
    protected List<EnemyBehaviour> enemies;

    public List<EnemyBehaviour> GetEnemiesInRange()
    {
        return enemies;
    }
}
