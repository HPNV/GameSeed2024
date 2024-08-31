using Manager;
using UnityEngine;

namespace Enemy.States
{
    public class DieState : IState
    {
        private readonly EnemyBehaviour enemy;
        
        public DieState(EnemyBehaviour enemy)
        {
            this.enemy = enemy;
        }
        
        public void OnUpdate()
        {
           
        }

        public void OnEnter()
        {
            var position = enemy.transform.position;
            SingletonGame.Instance.ExperienceManager.Spawn(3, new Vector3(position.x, position.y, position.z));
            SingletonGame.Instance.ResourceManager.Spawn(1, new Vector3(position.x, position.y, position.z));
            
            Object.Destroy(enemy.gameObject);
        }

        public void OnExit()
        {
           
        }

        public void OnCollisionStay2D(Collision2D collision)
        {
        }
    }
}