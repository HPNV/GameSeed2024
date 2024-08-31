using UnityEngine;

namespace Enemy.States
{
    public class MeleeAttackState : BaseState
    {
        public MeleeAttackState(EnemyBehaviour enemy) : base(enemy){} 
        
        
        public override void OnUpdate()
        {
            var stateInfo = Enemy.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Melee_Attack") && stateInfo.normalizedTime >= 1)
            {
                Enemy.ChangeState(State.Move);
            }
            
            if (Enemy.CurrentHealth <= 0)
            {
                Enemy.ChangeState(State.Die);
            }
        }

        public override void OnEnter()
        {
            Debug.Log("Enter attack state");
            Enemy.Animator.SetBool("IsAttacking", true);
        }

        public override void OnExit()
        {
            Debug.Log("Exit attack state");
            Enemy.Animator.SetBool("IsAttacking", false);
        }

        public override void OnCollisionStay2D(Collision2D collision)
        {
        }
    }
}