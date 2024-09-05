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
            
            Enemy.transform.localScale = Vector3.Lerp(Enemy.transform.localScale, Vector3.one * 8, Time.deltaTime / 3);
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1)
                Enemy.ChangeState(State.Explode);
        }
        
    }
}