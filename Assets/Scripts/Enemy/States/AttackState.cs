using UnityEngine;

namespace Enemy.States
{
    public class AttackState : BaseState
    {
        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
        protected AttackState(EnemyBehaviour enemy) : base(enemy){} 
        
        
        public override void OnUpdate()
        {
            if (Enemy.CurrentHealth <= 0)
            {
                Enemy.ChangeState(State.Die);
            }
        }

        public override void OnEnter()
        {
            Enemy.Animator.SetBool(IsAttacking, true);
        }

        public override void OnExit()
        {
            Enemy.Animator.SetBool(IsAttacking, false);
        }
    }
}