using System.Linq;
using Projectile;
using UnityEngine;

namespace Plant.States.Cobcorn
{
    public class CobcornAttackState : PlantAttackState
    {
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
            {
                _hasSpawnedProjectile = false;
            }
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1)
            {
                SpawnProjectile();
            }
        }
        
        private void SpawnProjectile()
        {
            if (_hasSpawnedProjectile)
                return;
            
            var target = Plant.TargetService.GetTargets().FirstOrDefault();
            
            if (target is null)
                return;
            
            _hasSpawnedProjectile = true;
            
            SingletonGame.Instance.ProjectileManager.SpawnWithTarget(ProjectileType.Cobcorn, Plant.transform.position, target.transform.position);
        }
    }
}