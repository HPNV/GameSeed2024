using System.Linq;
using Manager;
using Projectile;
using UnityEngine;

namespace Plant.States.Sneezeweed
{
    public class SneezeweedAttackState : PlantAttackState
    {
        private bool _hasSpawnedProjectile;
        public SneezeweedAttackState(Plant plant) : base(plant){}

        public override void OnEnter()
        {
            _hasSpawnedProjectile = false;
        }

        public override void Update()
        {
            base.Update();
            
            var stateInfo = Plant.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime < 0.2)
            {
                _hasSpawnedProjectile = false;
            }
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 0.2)
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
            SoundFXManager.instance.PlayGameSoundOnce("Audio/Plant/Push Attack");            
            SingletonGame.Instance.ProjectileManager.Spawn(
                ProjectileName.Sneezeweed, 
                Plant.transform.position, 
                Plant.Data.damage,
                direction: direction);
        }
    }
}