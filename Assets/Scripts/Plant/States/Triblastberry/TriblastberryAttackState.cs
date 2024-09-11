using System;
using System.Collections;
using System.Linq;
using Enemy;
using Manager;
using Projectile;
using UnityEngine;

namespace Plant.States.Triblastberry
{
    public class TriblastberryAttackState : PlantAttackState
    {
        private bool _hasSpawnedProjectile;
        private Coroutine _spawnProjectileCoroutine;
        private const float ProjectileAngle = 22.5f;
        public TriblastberryAttackState(Plant plant) : base(plant){}

        public override void OnEnter()
        {
            base.OnEnter();
            _hasSpawnedProjectile = false;
        }

        public override void Update()
        {
            base.Update();
            
            var stateInfo = Plant.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime < 0.5f)
            {
                _hasSpawnedProjectile = false;
            }
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 0.5f)
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
            SoundFXManager.instance.PlayGameSoundOnce("Audio/Plant/Cobcorn Attack");
            for (var i = 0; i < 3; i++)
            {
                var angle = i switch
                {
                    0 => -ProjectileAngle,
                    1 => 0,
                    2 => ProjectileAngle,
                    _ => 0
                };
                
                var rotation = Quaternion.Euler(0, 0, angle);
                
                var rotatedDirection = rotation * direction;
                
                SingletonGame.Instance.ProjectileManager.Spawn(
                    ProjectileName.Triblastberry, 
                    Plant.transform.position, 
                    Plant.Data.damage,
                    direction: rotatedDirection
                );
            }
            
        }
        
        public override void OnExit()
        {
            base.OnExit();
            
            if (_spawnProjectileCoroutine is not null) {
                Plant.StopCoroutine(_spawnProjectileCoroutine);

            }
        }
    }
}