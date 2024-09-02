using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using UnityEngine;

public class SingleTargetProvider : TargetService
{
    [SerializeField]
    private Transform curr;

    public override List<EnemyBehaviour> GetTargets()
    {
        var enemies = enemyDetectorService.GetEnemiesInRange();
        
        enemies.Sort((a, b) =>
        {
            var dist1 = Vector3.Distance(curr.position, a.transform.position);
            var dist2 = Vector3.Distance(curr.position, b.transform.position);
            return dist1.CompareTo(dist2);
        });

        return new List<EnemyBehaviour> { enemies.First() };
    }
}
