using System.Collections;
using UnityEngine;

namespace Enemy.States.Melee
{
    public class MeleeMoveState : MoveState
    {
        private bool _canTransition = true;
        private Coroutine _delayCoroutine;
        
        public MeleeMoveState(EnemyBehaviour enemy) : base(enemy){} 

        public override void OnEnter()
        {
            base.OnEnter();
            _canTransition = false;
            _delayCoroutine = Enemy.StartCoroutine(DelayChangeState());
            
        }
        public override void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(EnemyBehaviour.TargetTag) && _canTransition)
            {
                Enemy.ChangeState(State.Attack);
            }   
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