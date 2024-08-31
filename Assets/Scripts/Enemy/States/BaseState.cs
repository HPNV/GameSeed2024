using UnityEngine;

namespace Enemy.States
{
    public abstract class BaseState : IState
    {
        protected readonly EnemyBehaviour Enemy;
        
        protected BaseState(EnemyBehaviour enemy)
        {
            Enemy = enemy;
        }

        public void OnUpdate()
        {
            
        }

        public void OnEnter()
        {
           
        }

        public void OnExit()
        {
           
        }

        public void OnCollisionStay2D(Collision2D collision)
        {
            
        }
    }
}