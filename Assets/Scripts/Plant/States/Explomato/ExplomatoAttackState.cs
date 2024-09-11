using System;
using System.Linq;
using Manager;
using Projectile;
using UnityEngine;

namespace Plant.States.Explomato
{
    public class ExplomatoAttackState : PlantAttackState
    {
        private bool _hasSpawnedProjectile;
        public ExplomatoAttackState(Plant plant) : base(plant){}

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

            var target = Plant.TargetService.GetTargets().FirstOrDefault();
            
            if (target is null)
                return;
            
            _hasSpawnedProjectile = true;

            SingletonGame.Instance.ProjectileManager.Spawn(
                ProjectileName.Explomato, 
                Plant.transform.position, 
                Plant.Data.damage,
                target: target.transform.position);
        }
    }
}