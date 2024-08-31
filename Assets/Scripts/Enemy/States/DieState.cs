using Manager;
using UnityEngine;

namespace Enemy.States
{
    public class DieState : BaseState
    {
        public DieState(EnemyBehaviour enemy) : base(enemy){} 
        
        public override void OnUpdate()
        {
           
        }

        public override void OnEnter()
        {
            var position = Enemy.transform.position;
            SingletonGame.Instance.ExperienceManager.Spawn(3, new Vector3(position.x, position.y, position.z));
            SingletonGame.Instance.ResourceManager.Spawn(1, new Vector3(position.x, position.y, position.z));
            
            Object.Destroy(Enemy.gameObject);
        }

        public override void OnExit()
        {
           
        }

        public override void OnCollisionStay2D(Collision2D collision)
        {
        }
    }
}