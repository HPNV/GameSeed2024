using UnityEngine;

namespace Enemy.States
{
    public class MoveState : BaseState
    {
        protected MoveState(EnemyBehaviour enemy) : base(enemy){} 
        
        public override void OnUpdate()
        {
            if (Enemy.Target is null)
            {
                return;
            }
            
            var targetPosition = Enemy.Target.position;
            
             
            Enemy.transform.position = Vector2.MoveTowards(
                Enemy.transform.position, 
                targetPosition, 
                Enemy.enemyData.movementSpeed * Time.deltaTime);
            
            var direction = (targetPosition - Enemy.transform.position).normalized;
            
            Enemy.SpriteRenderer.flipX = direction.x > 0;
            
            if (Enemy.CurrentHealth <= 0)
            {
                Enemy.ChangeState(State.Die);
            }
        }
    }
}