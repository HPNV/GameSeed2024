using UnityEngine;

namespace Enemy.States
{
    public class MoveState : IState
    {
        private readonly EnemyBehaviour enemy;
        
        public MoveState(EnemyBehaviour enemy)
        {
            this.enemy = enemy;
        }
        
        public void OnUpdate()
        {
            var targetPosition = enemy.Target.position;
            
            var direction = (targetPosition - enemy.transform.position).normalized;
            
            enemy.transform.position += direction * (enemy.enemyData.movementSpeed * Time.deltaTime);
            
            if (direction.x > 0)
            {
                enemy.SpriteRenderer.flipX = true;
            }
            else
            {
                enemy.SpriteRenderer.flipX = false;
            }
            
            if (enemy.CurrentHealth <= 0)
            {
                enemy.ChangeState(State.Die);
            }
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {

        }

        public void OnCollisionStay2D(Collision2D collision)
        {
            // TODO CHANGE TO PLANT
            if (collision.gameObject.CompareTag("Dummy"))
            {
                enemy.ChangeState(State.Attack);
                Debug.Log("Change to attack");
            }   
        }
    }
}