using Manager;
using Projectile;
using UnityEngine;
using Utils;

namespace Enemy.States.Ranged
{
    public class RangedAttackState : AttackState
    {
        private float angleDifference = 30f;
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

            
            for (var i = 0; i < Enemy.enemyData.projectileCount; i++)
            {
                var angle = (i % 2 == 0 ? 1 : -1) * angleDifference * (i / 2);
                var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                var rotatedDirection = rotation * direction;
                
                SingletonGame.Instance.ProjectileManager.Spawn(
                    ProjectileName.Enemy, 
                    Enemy.transform.position,
                    Enemy.enemyData.attackPower,
                    direction: rotatedDirection
                    );
            }
        }
    }
}