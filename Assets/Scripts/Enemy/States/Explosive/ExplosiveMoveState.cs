using System.Collections;
using UnityEngine;

namespace Enemy.States.Explosive
{
    public class ExplosiveMoveState : MoveState
    {
        public ExplosiveMoveState(EnemyBehaviour enemy) : base(enemy){}

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            var targetPosition = Enemy.Target.position;
            
            var distance = Vector2.Distance(Enemy.transform.position, targetPosition);
            
            if (distance < Enemy.enemyData.attackRange)
            {
                Enemy.ChangeState(State.Attack);
            }
            
        }
        
        public override void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Dummy"))
            {
                Enemy.ChangeState(State.Attack);
                Debug.Log("Change to attack");
            }   
        }
    }
}