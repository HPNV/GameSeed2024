
using UnityEngine;

namespace Enemy.States
{
    public class MoveState : BaseState
    {
        protected MoveState(EnemyBehaviour enemy) : base(enemy){} 
        
        public override void OnUpdate()
        {
            var target = Enemy.PlantTargetService.GetTarget();
            if (target is null)
            {
                return;
            }
            
            var targetPosition = target.transform.position;
            
             
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