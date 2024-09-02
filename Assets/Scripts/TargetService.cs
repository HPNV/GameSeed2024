using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public abstract class TargetService : ScriptableObject
{
    protected EnemyDetectorService enemyDetectorService;

    public abstract List<EnemyBehaviour> GetTargets();
}
