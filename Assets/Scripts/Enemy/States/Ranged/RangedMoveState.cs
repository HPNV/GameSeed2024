using System.Collections;
using UnityEngine;

namespace Enemy.States.Ranged
{
    public class RangedMoveState : MoveState
    {
        private bool canTransition = true;
        private Coroutine _delayCoroutine;
        public RangedMoveState(EnemyBehaviour enemy) : base(enemy){}

        public override void OnEnter()
        {
            base.OnEnter();
            canTransition = false;
            _delayCoroutine = Enemy.StartCoroutine(DelayChangeState());
        }

        public override void OnUpdate()
        {
            var targetPosition = Enemy.Target.position;
            
            var distance = Vector2.Distance(Enemy.transform.position, targetPosition);

            if (!canTransition)
                return;
            
            if (distance < Enemy.enemyData.attackRange)
            {
                Enemy.ChangeState(State.Attack);
                return;
            }
            
            base.OnUpdate();
        }
        
        private IEnumerator DelayChangeState()
        {
            yield return new WaitForSeconds(2);
            canTransition = true;
        }
        
        public override void OnExit()
        {
            base.OnExit();
            
            if(_delayCoroutine != null)
                Enemy.StopCoroutine(_delayCoroutine);
        }
    }
}