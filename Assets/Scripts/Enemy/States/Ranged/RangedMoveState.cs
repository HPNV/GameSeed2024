using System.Collections;
using UnityEngine;

namespace Enemy.States.Ranged
{
    public class RangedMoveState : MoveState
    {
        private bool _canTransition = true;
        private Coroutine _delayCoroutine;
        public RangedMoveState(EnemyBehaviour enemy) : base(enemy){}

        public override void OnEnter()
        {
            base.OnEnter();
            _canTransition = false;
            _delayCoroutine = Enemy.StartCoroutine(DelayChangeState());
        }

        public override void OnUpdate()
        {
            var target = Enemy.PlantTargetService.GetTarget();
            if (target is null)
            {
                return;
            }
            
            var targetPosition = target.transform.position;
            
            var distance = Vector2.Distance(Enemy.transform.position, targetPosition);

            if (!_canTransition)
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
            _canTransition = true;
        }
        
        public override void OnExit()
        {
            base.OnExit();
            
            if(_delayCoroutine != null)
                Enemy.StopCoroutine(_delayCoroutine);
        }
    }
}