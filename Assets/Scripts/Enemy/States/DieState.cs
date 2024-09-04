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
            SingletonGame.Instance.ExperienceManager.SpawnBatch(3, new Vector3(position.x, position.y, position.z));
            SingletonGame.Instance.ResourceManager.SpawnBatchWithChance(1, new Vector3(position.x, position.y, position.z));
        }

        public override void OnUpdate()
        {
            var stateInfo = Enemy.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Die") && stateInfo.normalizedTime >= 1f)
            {
                SoundFXManager.instance.PlayGameSoundOnce(Resources.Load<AudioClip>("Audio/Enemy/Slime Death"));
                SingletonGame.Instance.EnemyManager.Despawn(Enemy);
            }
                
        }
    }
}