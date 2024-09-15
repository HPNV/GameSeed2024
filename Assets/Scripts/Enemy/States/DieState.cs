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
            SingletonGame.Instance.addEnemyKilled();
            Enemy.InnerCircleCollider.enabled = false;
            var position = Enemy.transform.position;
            SingletonGame.Instance.ExperienceManager.SpawnBatch(Random.Range(1, Enemy.enemyData.maxExperienceDrop), new Vector3(position.x, position.y, position.z));
            SingletonGame.Instance.ResourceManager.SpawnBatchWithChance(1, new Vector3(position.x, position.y, position.z));
        }

        public override void OnUpdate()
        {
            var stateInfo = Enemy.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Die") && stateInfo.normalizedTime >= 1f)
            {
                PlayerManager.Instance.OnEnemyKill();
                SoundFXManager.instance.PlayGameSoundOnce("Audio/Enemy/Slime Death");
                SingletonGame.Instance.homeBase.GainScore(Enemy.enemyData.maxExperienceDrop * 20);
                SingletonGame.Instance.EnemyManager.Despawn(Enemy);
            }
        }
    }
}