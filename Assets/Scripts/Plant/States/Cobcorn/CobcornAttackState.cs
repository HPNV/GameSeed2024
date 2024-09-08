using System;
using System.Linq;
using Manager;
using Projectile;
using UnityEngine;

namespace Plant.States.Cobcorn
{
    public class CobcornAttackState : PlantAttackState
    {
        private const int SpawnProjectileCount = 3;
        private bool _hasSpawnedProjectile;
        public CobcornAttackState(Plant plant) : base(plant){}

        public override void OnEnter()
        {
            base.OnEnter();
            _hasSpawnedProjectile = false;
        }

        public override void Update()
        {
            base.Update();
            
            var stateInfo = Plant.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime < 0.5)
                _hasSpawnedProjectile = false;
    
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 0.8)
                SpawnProjectile();
        }
        
        private void SpawnProjectile()
        {
            if (_hasSpawnedProjectile)
                return;
            
            var targets = Plant.TargetService.GetTargets();
            
            if (targets.Count == 0)
                return;
            
            _hasSpawnedProjectile = true;

            var hitTarget = targets.Take(SpawnProjectileCount).ToList();

            foreach (var target in hitTarget)
                SingletonGame.Instance.ProjectileManager.Spawn(ProjectileName.Cobcorn, Plant.transform.position, target: target.transform.position);
        }
    }
}