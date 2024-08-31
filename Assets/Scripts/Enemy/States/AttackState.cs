using UnityEngine;

namespace Enemy.States
{
    public class AttackState : IState
    {
        private readonly EnemyBehaviour enemy;
        
        public AttackState(EnemyBehaviour enemy)
        {
            this.enemy = enemy;
        }
        
        
        public void OnUpdate()
        {
            var stateInfo = enemy.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Melee_Attack") && stateInfo.normalizedTime >= 1)
            {
                enemy.ChangeState(State.Move);
            }
            
            if (enemy.CurrentHealth <= 0)
            {
                enemy.ChangeState(State.Die);
            }
        }

        public void OnEnter()
        {
            Debug.Log("Enter attack state");
            enemy.Animator.SetBool("IsAttacking", true);
        }

        public void OnExit()
        {
            Debug.Log("Exit attack state");
            enemy.Animator.SetBool("IsAttacking", false);
        }

        public void OnCollisionStay2D(Collision2D collision)
        {
        }
    }
}