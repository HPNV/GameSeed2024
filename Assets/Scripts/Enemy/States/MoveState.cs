using UnityEngine;

namespace Enemy.States
{
    public class MoveState : BaseState
    {
        public MoveState(EnemyBehaviour enemy) : base(enemy){} 
        
        public override void OnUpdate()
        {
            var targetPosition = Enemy.Target.position;
            
            var direction = (targetPosition - Enemy.transform.position).normalized;
             
            Enemy.transform.position = Vector2.MoveTowards(
                Enemy.transform.position, 
                targetPosition, 
                Enemy.enemyData.movementSpeed * Time.deltaTime);
            
            Enemy.SpriteRenderer.flipX = direction.x > 0;
            
            if (Enemy.CurrentHealth <= 0)
            {
                Enemy.ChangeState(State.Die);
            }
        }

        public override void OnEnter()
        {
            
        }

        public override void OnExit()
        {

        }

        public override void OnCollisionStay2D(Collision2D collision)
        {
            // TODO CHANGE TO PLANT
            if (collision.gameObject.CompareTag("Dummy"))
            {
                Enemy.ChangeState(State.Attack);
                Debug.Log("Change to attack");
            }   
        }
    }
}