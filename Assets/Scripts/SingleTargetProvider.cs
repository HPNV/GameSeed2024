using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using Enemy.States;
using UnityEngine;

public class SingleTargetProvider : TargetService
{
    public override List<EnemyBehaviour> GetTargets()
    {
        var enemies = enemyDetectorService.GetEnemiesInRange();
        
        enemies.Sort((a, b) =>
        {
            var dist1 = Vector3.Distance(curr.position, a.transform.position);
            var dist2 = Vector3.Distance(curr.position, b.transform.position);
            return dist1.CompareTo(dist2);
        });

        enemies = enemies.Where(e => e is not null && e.CurrentState != State.Die).ToList();

        return enemies.Count == 0 ? new List<EnemyBehaviour>() : new List<EnemyBehaviour> { enemies.FirstOrDefault() };
    }
}
