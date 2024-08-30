using UnityEngine;

namespace Enemy.States
{
    public class MoveState : IState
    {
        public void OnUpdate(EnemyBehaviour enemy)
        {
            var targetPosition = enemy.Target.position;
            
            var direction = (targetPosition - enemy.transform.position).normalized;
            
            enemy.transform.position += direction * (enemy.enemyData.movementSpeed * Time.deltaTime);
        }

        public void OnEnter(EnemyBehaviour enemy)
        {
  
        }

        public void OnExit(EnemyBehaviour enemy)
        {

        }
    }
}