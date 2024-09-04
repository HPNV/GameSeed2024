using Manager;
using UnityEngine;

namespace Enemy.States
{
    public class DieState : BaseState
    {
        private static readonly int Die = Animator.StringToHash("Die");
        public DieState(EnemyBehaviour enemy) : base(enemy){} 

        public override void OnEnter()
        {
            Enemy.Animator.SetTrigger(Die);
            
            var position = Enemy.transform.position;
            SingletonGame.Instance.ExperienceManager.Spawn(3, new Vector3(position.x, position.y, position.z));
            SingletonGame.Instance.ResourceManager.Spawn(1, new Vector3(position.x, position.y, position.z));
        }

        public override void OnUpdate()
        {
            var stateInfo = Enemy.Animator.GetCurrentAnimatorStateInfo(0);

            Debug.Log($"TRYING TO UPDATE DIE {stateInfo.IsName("Die")} {stateInfo.normalizedTime} {stateInfo.normalizedTime >= 1f}");
            if (stateInfo.IsName("Die") && stateInfo.normalizedTime >= 1f)
            {
                Debug.Log("hahahaha");
                SingletonGame.Instance.EnemyManager.Despawn(Enemy);
            }
                
        }
    }
}