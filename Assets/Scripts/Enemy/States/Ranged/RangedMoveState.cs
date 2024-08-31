using UnityEngine;

namespace Enemy.States.Ranged
{
    public class RangedMoveState : MoveState
    {
        public RangedMoveState(EnemyBehaviour enemy) : base(enemy){} 
        
        public override void OnUpdate()
        {
            var targetPosition = Enemy.Target.position;
            
            var distance = Vector2.Distance(Enemy.transform.position, targetPosition);

            if (distance < Enemy.enemyData.attackRange)
            {
                Enemy.ChangeState(State.Attack);
                return;
            }
            
            base.OnUpdate();
        }
    }
}