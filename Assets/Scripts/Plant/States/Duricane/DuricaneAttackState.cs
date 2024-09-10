using System.Collections;
using System.Linq;
using Enemy;
using Manager;
using Projectile;
using UnityEngine;

namespace Plant.States.Duricane
{
    public class DuricaneAttackState : PlantAttackState
    {
        private bool _hasSpawnedProjectile;
        private Coroutine _spawnProjectileCoroutine;
        private int _projectileCount = 20;
        public DuricaneAttackState(Plant plant) : base(plant){}

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
            
            _spawnProjectileCoroutine = Plant.StartCoroutine(SpawnProjectileCoroutine(target));
        }
        
        private IEnumerator SpawnProjectileCoroutine(EnemyBehaviour target)
        {
            var direction = (target.transform.position - Plant.transform.position).normalized;
            
            var angleStep = 360f / (_projectileCount - 2);
            for (var i = 0; i < _projectileCount; i++)
            {
                var angle = angleStep * i;
                
                var rotation = Quaternion.Euler(0, 0, angle);
                
                var rotatedDirection = rotation * direction;
                
                SingletonGame.Instance.ProjectileManager.Spawn(
                    ProjectileName.Duricane, 
                    Plant.transform.position, 
                    Plant.Data.damage,
                    direction: rotatedDirection);
                yield return new WaitForSeconds(0.025f);
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            
            if (_spawnProjectileCoroutine is not null)
                Plant.StopCoroutine(_spawnProjectileCoroutine);
        }
    }
}