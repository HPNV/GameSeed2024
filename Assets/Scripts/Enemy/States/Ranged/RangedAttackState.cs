using Projectile;
using UnityEngine;
using Utils;

namespace Enemy.States.Ranged
{
    public class RangedAttackState : AttackState
    {
        private bool _hasSpawnedProjectile;
        public RangedAttackState(EnemyBehaviour enemy) : base(enemy){}


        public override void OnEnter()
        {
            base.OnEnter();
            _hasSpawnedProjectile = false;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            var stateInfo = Enemy.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 0.4f)
                SpawnProjectile();
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1f)
                Enemy.ChangeState(State.Move);
        }
        
        private void SpawnProjectile()
        {
            var target = Enemy.PlantTargetService.GetTarget();
            if (_hasSpawnedProjectile || target is null)
                return;
            
            _hasSpawnedProjectile = true;
            var direction = (target.transform.position - Enemy.transform.position).normalized;
            
            SingletonGame.Instance.ProjectileManager.Spawn(ProjectileType.EnemyRanged, Enemy.transform.position, direction);
        }
    }
}