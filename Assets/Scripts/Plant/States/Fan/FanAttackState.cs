using System.Linq;
using Manager;
using Projectile;
using UnityEngine;

namespace Plant.States.Fan
{
    public class FanAttackState : PlantAttackState
    {
        private bool _hasSpawnedProjectile;
        public FanAttackState(Plant plant) : base(plant){}

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
            var direction = (target.transform.position - Plant.transform.position).normalized;
            
            SingletonGame.Instance.ProjectileManager.Spawn(ProjectileName.Fan, Plant.transform.position, direction: direction);
        }
    }
}