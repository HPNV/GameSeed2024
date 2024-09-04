using System.Collections;
using UnityEngine;

namespace Enemy.States.Explosive
{
    public class ExplosiveMoveState : MoveState
    {
        public ExplosiveMoveState(EnemyBehaviour enemy) : base(enemy){}
        

        public override void OnUpdate()
        {
            var stateInfo = Enemy.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Walk") && isInJumpAnimation())
                base.OnUpdate();
            
            var target = Enemy.PlantTargetService.GetTarget();
            
            if (target is null)
            {
                return;
            }
            
            var targetPosition = target.transform.position;
            
            var distance = Vector2.Distance(Enemy.transform.position, targetPosition);
            
            if (distance < Enemy.enemyData.attackRange)
            {
                Enemy.ChangeState(State.Attack);
            }
            
        }
        
        public override void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(EnemyBehaviour.TargetTag))
            {
                Enemy.ChangeState(State.Attack);
                Debug.Log("Change to attack");
            }   
        }
        
        private bool isInJumpAnimation()
        {
            var stateInfo = Enemy.Animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.normalizedTime % 1 >= 0.5f && stateInfo.normalizedTime % 1 <= 1f;
        }
    }
}