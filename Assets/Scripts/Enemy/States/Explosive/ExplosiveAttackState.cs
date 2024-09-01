using Projectile;
using UnityEngine;

namespace Enemy.States.Explosive
{
    public class ExplosiveAttackState : AttackState
    {
        public ExplosiveAttackState(EnemyBehaviour enemy) : base(enemy){}
        

        public override void OnUpdate()
        {
            base.OnUpdate();
            var stateInfo = Enemy.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1)
                Object.Destroy(Enemy.gameObject);
        }
        
    }
}