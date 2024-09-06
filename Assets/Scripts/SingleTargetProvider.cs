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
        
        var sortedEnemies = enemies
            .OrderBy(e => Vector2.Distance(curr.position, e.transform.position))
            .ToList();
        
        var validEnemies = sortedEnemies
            .Where(e => e is not null && e.CurrentState != State.Die)
            .ToList();

        return enemies.Count == 0 ? new List<EnemyBehaviour>() : new List<EnemyBehaviour> { validEnemies.FirstOrDefault() };
    }
}
