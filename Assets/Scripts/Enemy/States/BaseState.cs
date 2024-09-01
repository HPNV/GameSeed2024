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

        public virtual void OnUpdate()
        {
            
        }

        public virtual void OnEnter()
        {
           
        }

        public virtual void OnExit()
        {
           
        }

        public virtual void OnCollisionStay2D(Collision2D collision)
        {
            
        }
    }
}