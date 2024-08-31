using Projectile;
using UnityEngine;

namespace Enemy.States.Explosive
{
    public class ExplosiveAttackState : AttackState
    {
        private bool _hasSpawnedProjectile;
        public ExplosiveAttackState(EnemyBehaviour enemy) : base(enemy){}


        public override void OnEnter()
        {
            base.OnEnter();
            _hasSpawnedProjectile = false;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            var stateInfo = Enemy.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1)
                Object.Destroy(Enemy.gameObject);
        }
        
    }
}