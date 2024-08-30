using Manager;
using UnityEngine;

namespace Enemy.States
{
    public class DieState : IState
    {
        public void OnUpdate(EnemyBehaviour enemy)
        {
           
        }

        public void OnEnter(EnemyBehaviour enemy)
        {
            var position = enemy.transform.position;

            ExperienceManager.Spawn(3, new Vector3(position.x, position.y, position.z));
        }

        public void OnExit(EnemyBehaviour enemy)
        {
           
        }
    }
}