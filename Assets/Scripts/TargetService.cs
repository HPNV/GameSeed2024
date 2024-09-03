using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public abstract class TargetService : MonoBehaviour
{
    [SerializeField] 
    protected Transform curr;
    [SerializeField]
    protected EnemyDetectorService enemyDetectorService;

    public abstract List<EnemyBehaviour> GetTargets();
}
